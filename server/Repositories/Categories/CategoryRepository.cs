using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopAppContext _context;

        public CategoryRepository(ShopAppContext context) {
            _context = context;
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            var entries = _context.Categories.AsNoTracking().Select( q => new CategoryVM
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                ParentId = q.ParentId,
                Lft = q.Lft,
                Rgt = q.Rgt,
                NotUse = q.NotUse,
                ProductCount = _context.Products.Count(p => p.CategoryId == q.Id),
            });

            return await entries.ToListAsync();
        }

        public List<CategoryTreeVM> BuildTree(List<CategoryVM> categories)
        {
            var lookup = categories.ToDictionary(c => c.Id, c => new CategoryTreeVM
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Lft = c.Lft,
                Rgt = c.Rgt,
                ParentId = c.ParentId,
                Children = [],
                ProductCount = c.ProductCount,
            });

            List<CategoryTreeVM> rootNodes = [];

            foreach (var node in lookup.Values)
            {
                if (node.ParentId.HasValue)
                {
                    if (lookup.TryGetValue(node.ParentId.Value, out var parent))
                    {
                        parent.Children.Add(node);
                    }
                }
                else
                {
                    rootNodes.Add(node); // Node không có Parent là Root
                }
            }

            return rootNodes;
        }
        public async Task<Category> Show(long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id); 
            return category ?? throw new ArgumentException("Not found");
        }

        public async Task<Brand> Create(StoreCategoryRequest request)
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

        public async Task Update(long id, UpdateCategoryRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                brand.Name = request.Name;
                brand.Code = request.Code;
                
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
