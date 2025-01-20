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
            Tax newTax = new Tax{
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
    }
}
