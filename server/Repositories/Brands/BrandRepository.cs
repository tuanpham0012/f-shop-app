using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Repositories.RedisCache;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Products
{
    public class BrandRepository(ShopAppContext context, IRedisCache cache, IFileHelper fileHelper) : IBrandRepository
    {
        public async Task<List<Brand>> GetAll(BrandRequest request)
        {
            string cacheKey = $"{Constants.BrandCache}-request-{request.NotUse.ToString()}";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                var query = context.Brands.AsNoTracking().AsQueryable();
                if (request.NotUse != null)
                {
                    query = query.Where(x => x.NotUse == request.NotUse);
                }
                return await query.ToListAsync();
            });
            return cacheData ?? [];
        }

        public async Task<Brand> Show(long id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == id); 
            return brand ?? throw new ArgumentException("Not found");
        }

        public async Task<Brand> Create(StoreBrandRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var entry = new Brand
            {
                Name = request.Name,
                Code = request.Code,
                Image = await fileHelper.SaveFile(request.Image),
                NotUse = request.NotUse,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
            context.Add(entry);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return entry;
        }

        public async Task Update(long id, UpdateBrandRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var brand = context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                brand.Name = request.Name;
                brand.Code = request.Code;
                if(brand.Image != request.Image)
                {
                    fileHelper.DeleteFile(brand.Image);
                    brand.Image = await fileHelper.SaveFile(request.Image);
                }
                brand.NotUse = request.NotUse;
                brand.CreatedAt = DateTime.UtcNow;
                brand.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = context.Database.BeginTransaction();
            var brand = context.Brands.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (brand != null)
            {
                if(brand.Products.Count > 0)
                {
                    throw new ArgumentException("Brand has products");
                }
                fileHelper.DeleteFile(brand.Image);
                context.Brands.Remove(brand);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task<List<BrandVM>> GetBrandByCategory(string CategoryCode)
        {
            HttpContext httpContext = new HttpContextAccessor().HttpContext;
            var brands = await context.Brands.Include("Products").Where(x => x.Products.Any(y => y.Category.Code == CategoryCode)).Select(x => new BrandVM{
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Image = fileHelper.getLink(x.Image, httpContext),
                NotUse = x.NotUse,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                ProductCount = x.Products.Count
            }).ToListAsync();
            return brands ?? [];
        }
    }
}
