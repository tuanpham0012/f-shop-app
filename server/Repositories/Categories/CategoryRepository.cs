using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopAppContext _context;

        public CategoryRepository(ShopAppContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryVM>> GetAll(CategoryRequest request)
        {
            var entries = _context.Categories.AsNoTracking().Select(q => new CategoryVM
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                ParentId = q.ParentId,
                Lft = q.Lft,
                Rgt = q.Rgt,
                NotUse = q.NotUse,
                Image = q.Image,
                IsPopular = q.IsPopular,
                HidenMenu = q.HidenMenu,
                ProductCount = _context.Products.Count(p => p.CategoryId == q.Id),
            });

            if (request.NotUse != null)
            {
                entries = entries.Where(x => x.NotUse == request.NotUse);
            }

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
                Image = c.Image,
                IsPopular = c.IsPopular,
                HidenMenu = c.HidenMenu,
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

        public async Task<Category> Create(StoreCategoryRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var countChildrent = await _context.Categories.Where(c => c.ParentId == request.ParentId).CountAsync();
            int left = countChildrent++;

            // Thêm node mới
            var entry = new Category
            {
                Name = request.Name,
                Code = request.Code,
                Lft = left,
                Rgt = 0,
                ParentId = request.ParentId,
                NotUse = request.NotUse,
                Image = await FileHelper.SaveFile(request.Image),
                IsPopular = request.IsPopular,
                HidenMenu = request.HidenMenu,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Add(entry);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return entry;
        }

        public async Task Update(long id, UpdateCategoryRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                category.Name = request.Name;
                category.Code = request.Code;
                category.Lft = request.Lft;
                category.ParentId = request.ParentId;
                category.NotUse = request.NotUse;
                if(category.Image != request.Image)
                {
                    FileHelper.DeleteFile(category.Image);
                    category.Image = await FileHelper.SaveFile(request.Image);
                }
                category.IsPopular = request.IsPopular;
                category.HidenMenu = request.HidenMenu;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = _context.Database.BeginTransaction();
            var category = _context.Categories.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (category != null)
            {
                if (category.Products.Count > 0)
                {
                    throw new ArgumentException("Category has products");
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
