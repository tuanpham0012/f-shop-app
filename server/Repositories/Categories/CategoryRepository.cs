using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Repositories.RedisCache;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Categories
{
    public class CategoryRepository(ShopAppContext context, IRedisCache cache, IFileHelper fileHelper) : ICategoryRepository
    {
        public async Task<List<CategoryVM>> GetAll(CategoryRequest request)
        {
            string cacheKey = $"{Constants.CategoryCache}:GetAll-request-{cache.ReplaceString(JsonSerializer.Serialize(request))}";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                var query = context.Categories.AsNoTracking().Select(q => new CategoryVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    Code = q.Code,
                    ParentId = q.ParentId,
                    Lft = q.Lft,
                    Rgt = q.Rgt,
                    NotUse = q.NotUse,
                    Image = fileHelper.GetLink(q.Image),
                    IsPopular = q.IsPopular,
                    HidenMenu = q.HidenMenu,
                    ProductCount = context.Products.Count(p => p.CategoryId == q.Id),
                });

                if (request.NotUse != null)
                {
                    query = query.Where(x => x.NotUse == request.NotUse);
                }

                return await query.ToListAsync();
            });
            return cacheData ?? [];
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
        public async Task<CategoryVM> Show(long id)
        {
            var category = await context.Categories.Select(q => new CategoryVM
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                ParentId = q.ParentId,
                Lft = q.Lft,
                Rgt = q.Rgt,
                NotUse = q.NotUse,
                Image = !string.IsNullOrEmpty(q.Image) ? fileHelper.GetLink(q.Image) : null,
                IsPopular = q.IsPopular,
                HidenMenu = q.HidenMenu,
                // ProductCount = context.Products.Count(p => p.CategoryId == q.Id),
            }).FirstOrDefaultAsync(x => x.Id == id);
            return category ?? throw new ArgumentException("Not found");
        }

        public async Task<Category> Create(StoreCategoryRequest request)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            var countChildrent = await context.Categories.Where(c => c.ParentId == request.ParentId).CountAsync();
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
                Image = request.Image != null ? await fileHelper.SaveFile(request.Image) : "",
                IsPopular = request.IsPopular,
                HidenMenu = request.HidenMenu,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };
            context.Add(entry);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            cache.RemoveByPrefix(Constants.CategoryCache);
            return entry;
        }

        public async Task Update(long id, UpdateCategoryRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                category.Name = request.Name;
                category.Code = request.Code;
                category.Lft = request.Lft;
                category.ParentId = request.ParentId;
                category.NotUse = request.NotUse;
                if (!string.IsNullOrEmpty(request.Image) && (category.Image == null || !request.Image.Contains(category.Image)))
                {
                    fileHelper.DeleteFile(category.Image);
                    category.Image = await fileHelper.SaveFile(request.Image);
                }
                category.IsPopular = request.IsPopular;
                category.HidenMenu = request.HidenMenu;
                category.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                cache.RemoveByPrefix(Constants.CategoryCache);
            }
        }

        public async Task Delete(long Id)
        {
            using var transaction = context.Database.BeginTransaction();
            var category = context.Categories.Include("Products").FirstOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Not found");
            if (category != null)
            {
                if (category.Products.Count > 0)
                {
                    throw new ArgumentException("Category has products");
                }
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                cache.RemoveByPrefix(Constants.CategoryCache);
            }
        }

        public async Task<List<CategoryVM>> GetPopularCategory()
        {
            var cacheKey = $"{Constants.CategoryCache}:GetPopularCategory";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await context.Categories.AsNoTracking().Select(q => new CategoryVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    Code = q.Code,
                    ParentId = q.ParentId,
                    Lft = q.Lft,
                    Rgt = q.Rgt,
                    NotUse = q.NotUse,
                    Image = fileHelper.GetLink(q.Image),
                    IsPopular = q.IsPopular,
                    HidenMenu = q.HidenMenu,
                    ProductCount = context.Products.Count(p => p.CategoryId == q.Id),
                }).Where(c => c.IsPopular == true && c.NotUse == false).ToListAsync();
            });
            return cacheData ?? [];
        }

        public async Task<List<CategoryVM>> GetTopCategoryWithProduct()
        {
            var cacheKey = $"{Constants.CategoryCache}:GetTopCategoryWithProduct";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await context.Categories.AsNoTracking().Include("Products").Where(c => c.IsPopular == true && c.NotUse == false).Select(q => new CategoryVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    Code = q.Code,
                    ParentId = q.ParentId,
                    Lft = q.Lft,
                    Rgt = q.Rgt,
                    NotUse = q.NotUse,
                    Image = fileHelper.GetLink(q.Image),
                    IsPopular = q.IsPopular,
                    HidenMenu = q.HidenMenu,
                    Products = q.Products.Select(p => new ProductVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        ImageThumb = fileHelper.GetLink(p.ImageThumb),
                        UnitSell = p.UnitSell,
                        UnitBuy = p.UnitBuy,
                        Alias = p.Alias,
                        HasVariants = p.HasVariants,
                        IsNew = p.IsNew,
                        IsFeatured = p.IsFeatured,
                        IsSale = p.IsSale,
                        BrandId = p.BrandId,
                        CategoryId = p.CategoryId,
                    }).OrderByDescending(x => x.Id).Take(20).ToList(),
                }).Take(3).ToListAsync();
            });
            return cacheData ?? new List<CategoryVM>();
        }

        public async Task<List<CategoryVM>> GetCategoryHasFeaturedProduct()
        {
            var cacheKey = $"{Constants.CategoryCache}:GetCategoryHasFeaturedProduct";
            var cacheData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await context.Categories.AsNoTracking().Where(c => c.Products.Any(p => p.IsFeatured == true || p.IsNew == true)).Select(q => new CategoryVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    Code = q.Code,
                    ParentId = q.ParentId,
                    Lft = q.Lft,
                    Rgt = q.Rgt,
                    NotUse = q.NotUse,
                    Image = fileHelper.GetLink(q.Image),
                    IsPopular = q.IsPopular,
                    HidenMenu = q.HidenMenu,
                    ProductCount = q.Products.Count(),
                }).ToListAsync();
            });
            return cacheData ?? new List<CategoryVM>();
        }
    }
}
