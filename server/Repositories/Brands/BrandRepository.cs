using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShopAppContext _context;

        public BrandRepository(ShopAppContext context) { _context = context;  }
        public async Task<List<Brand>> GetAll(BrandRequest request)
        {
            var query = _context.Brands.AsNoTracking().AsQueryable();
            if (request.NotUse != null)
            {
                query = query.Where(x => x.NotUse == request.NotUse);
            }
            return await query.ToListAsync();
        }

        public async Task<Brand> Show(long id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id); 
            return brand ?? throw new ArgumentException("Not found");
        }

        public async Task<Brand> Create(StoreBrandRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var entry = new Brand
            {
                Name = request.Name,
                Code = request.Code,
                Image = await FileHelper.SaveFile(request.Image),
                NotUse = request.NotUse,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
            _context.Add(entry);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return entry;
        }

        public async Task Update(long id, UpdateBrandRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                brand.Name = request.Name;
                brand.Code = request.Code;
                if(brand.Image != request.Image)
                {
                    FileHelper.DeleteFile(brand.Image);
                    brand.Image = await FileHelper.SaveFile(request.Image);
                }
                brand.NotUse = request.NotUse;
                brand.CreatedAt = DateTime.UtcNow;
                brand.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = _context.Database.BeginTransaction();
            var brand = _context.Brands.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (brand != null)
            {
                if(brand.Products.Count > 0)
                {
                    throw new ArgumentException("Brand has products");
                }
                FileHelper.DeleteFile(brand.Image);
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
