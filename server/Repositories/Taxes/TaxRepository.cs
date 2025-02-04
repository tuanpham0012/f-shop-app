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
            if(request.NotUse != null)
            {
                entries = entries.Where(x => x.NotUse == request.NotUse);
            }

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
            var tax = _context.Taxes.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("Not found");
            if (tax != null)
            {
                tax.Name = request.Name;
                tax.NotUse = request.NotUse;
                tax.Value = request.Value;
                tax.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = _context.Database.BeginTransaction();
            var tax = _context.Taxes.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (tax != null)
            {
                if(tax.Products.Count > 0)
                {
                    throw new ArgumentException("Thuế này đang được sử, không thể xóa!");
                }
                _context.Taxes.Remove(tax);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
