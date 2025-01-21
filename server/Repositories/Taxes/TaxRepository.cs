using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public class TaxRepository(ShopAppContext context) : ITaxRepository
    {
        private readonly ShopAppContext _context = context;

        public async Task<List<Tax>> GetAll(TaxRequest request)
        {
            var entries = _context.Taxes.AsNoTracking().AsQueryable();
            // if((bool)request.Using)
            // {
            //     // entries = entries.Where(x => x. == request.Using);
            // }

            return await entries.ToListAsync();
        }

        public async Task<Tax> Create(StoreTaxRequest tax)
        {
            Tax newTax = new()
            {
                Name = tax.Name,
                Value = tax.Value,
                NotUse = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Taxes.AddAsync(newTax);
            await _context.SaveChangesAsync();
            return newTax;
        }

        public async Task<Tax> Show(long id)
        {
            var tax = await _context.Taxes.FirstOrDefaultAsync(x => x.Id == id); 
            return tax ?? throw new ArgumentException("Not found");
        }

        public async Task Update(long id, UpdateTaxRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var brand = _context.Taxes.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                brand.Name = request.Name;
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
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
