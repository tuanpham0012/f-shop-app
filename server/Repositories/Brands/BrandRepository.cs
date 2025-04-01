using System.Text.Json;
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
        public async Task<List<BrandVM>> GetAll(BrandRequest request)
        {
            
            string cacheKey = $"{Constants.BrandCache}:GetAll-request-" + cache.ReplaceString(JsonSerializer.Serialize(request));
            Console.WriteLine(cacheKey);
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                var query = context.Brands.AsNoTracking().AsQueryable();
                if (request.NotUse != null)
                {
                    query = query.Where(x => x.NotUse == request.NotUse);
                }
                return await query.Select(x => new BrandVM
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Image = !string.IsNullOrEmpty(x.Image) ? fileHelper.GetLink(x.Image) : "",
                    NotUse = x.NotUse,
                    ProductCount = x.Products.Count
                }).OrderByDescending(x => x.Id).ToListAsync();
            });
            return cacheData ?? [];
        }

        public async Task<BrandVM> Show(long id)
        {
            var brand = await context.Brands.Select(x => new BrandVM
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Image = !string.IsNullOrEmpty(x.Image) ? fileHelper.GetLink(x.Image) : null,
                NotUse = x.NotUse,
                ProductCount = x.Products.Count
            }).FirstOrDefaultAsync(x => x.Id == id);
            return brand ?? throw new ArgumentException("Not found");
        }

        public async Task<Brand> Create(StoreBrandRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var entry = new Brand
            {
                Name = request.Name,
                Code = request.Code,
                Image = request.Image != null ? fileHelper.SaveFile(request.Image) : "",
                NotUse = request.NotUse,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
            context.Add(entry);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            cache.RemoveByPrefix(Constants.BrandCache);
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
                if (!string.IsNullOrEmpty(request.Image) && (brand.Image == null || !request.Image.Contains(brand.Image)))
                {
                    fileHelper.DeleteFile(brand.Image);
                    brand.Image = fileHelper.SaveFile(request.Image);
                }
                brand.NotUse = request.NotUse;
                brand.CreatedAt = DateTime.UtcNow;
                brand.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                cache.RemoveByPrefix(Constants.BrandCache);
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = context.Database.BeginTransaction();
            var brand = context.Brands.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (brand != null)
            {
                if (brand.Products.Count > 0)
                {
                    throw new ArgumentException("Brand has products");
                }
                fileHelper.DeleteFile(brand.Image);
                context.Brands.Remove(brand);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                cache.RemoveByPrefix(Constants.BrandCache);
            }
        }

        public async Task<List<BrandVM>> GetBrandByCategory(string CategoryCode)
        {
            string cacheKey = $"{Constants.BrandCache}-GetBrandByCategory-CategoryCode-{CategoryCode}";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await context.Brands.AsQueryable().AsNoTracking().Include("Products").Where(x => x.Products.Any(y => y.Category.Code == CategoryCode)).Select(x => new BrandVM
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Image = !string.IsNullOrEmpty(x.Image) ? fileHelper.GetLink(x.Image) : "",
                    NotUse = x.NotUse,
                    ProductCount = x.Products.Count
                }).OrderByDescending(x => x.Id).ToListAsync();
            });

            return cacheData ?? [];
        }
    }
}
