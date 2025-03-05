using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ShopAppApi.Data;
using ShopAppApi.Repositories.RedisCache;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Menus
{
    public class MenuRepository(ShopAppContext context, IRedisCache cache) : IMenuRepository
    {
        private readonly ShopAppContext _context = context;

        private readonly IRedisCache _cache = cache;

        public async Task Create(StoreMenuRequest menu)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var parent = await _context.Menus.FirstOrDefaultAsync(c => c.Id == menu.ParentId);
            int left = parent != null ? parent.Rgt : 1;
            int right = left + 1;

            // Update các node bên phải của vị trí cần thêm
            await _context.Database.ExecuteSqlRawAsync(
                "UPDATE menus SET _rgt = _rgt + 2 WHERE _rgt >= {0}", left);
            await _context.Database.ExecuteSqlRawAsync(
                "UPDATE menus SET _lft = _lft + 2 WHERE _lft > {0}", left);

            // Thêm node mới
            var newMenu = new Menu
            {
                Title = menu.Title,
                Lft = left,
                Rgt = right,
                ParentId = menu.ParentId,
                Url = menu.Url,
                Hidden = menu.Hidden,
                GroupMenu = menu.GroupMenu,
            };

            _context.Menus.Add(newMenu);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task Update(long Id, UpdateMenuRequest menu)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var _menu = await _context.Menus.SingleOrDefaultAsync(c => c.Id == Id) ?? throw new ArgumentException("Dữ liệu không tồn tại"); ;

            _menu.Title = menu.Title;
            _menu.Lft = menu.Lft;
            _menu.Rgt = menu.Rgt;
            _menu.ParentId = menu.ParentId;
            _menu.Hidden = menu.Hidden;
            _menu.GroupMenu = menu.GroupMenu;
            _menu.Url = menu.Url;
            _menu.Icon = menu.Icon;
            _menu.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

        }

        public async Task<List<Menu>> GetAll()
        {
            return await _context.Menus.AsQueryable().OrderBy(q => q.Lft).OrderBy(m => m.Position).AsNoTracking().ToListAsync();
        }

        public List<MenuTree> BuildTree(List<Menu> menus)
        {
            var lookup = menus.ToDictionary(c => c.Id, c => new MenuTree
            {
                Id = c.Id,
                Title = c.Title,
                Icon = c.Icon,
                Url = c.Url,
                Lft = c.Lft,
                Rgt = c.Rgt,
                ParentId = c.ParentId,
                GroupMenu = c.GroupMenu,
                Children = []
            });

            List<MenuTree> rootNodes = [];

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

        public async Task<Menu> Show(long Id)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(c => c.Id == Id);
            return menu ?? throw new ArgumentException("Menu not found");
        }

        public async Task<List<MenuTree>> GetAdminMenu()
        {
            string cacheKey = Constants.AdminMenuCache;
            try
            {
                var result = await _cache.GetOrCreateAsync(
                    cacheKey,
                    async () => {
                        var entries = await GetAll();
                        var tree = BuildTree(entries);
                        return tree.Where(x => x.Id == 1).FirstOrDefault()?.Children ?? new List<MenuTree>();
                            }
                        );
                    return result ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        public async Task<List<MenuTree>> GetUserMenu()
        {
            string cacheKey = Constants.UserMenuCache;
            try
            {
                var result = await _cache.GetOrCreateAsync(
                    cacheKey,
                    async () => {
                        var entries = await GetAll();
                        var tree = BuildTree(entries);
                        return tree.Where(x => x.Id == 2).FirstOrDefault()?.Children ?? new List<MenuTree>();
                            }
                        );
                    return result ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}