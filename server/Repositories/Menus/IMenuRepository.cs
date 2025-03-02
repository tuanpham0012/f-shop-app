using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.Menus
{
    public interface IMenuRepository
    {
        public Task<List<Menu>> GetAll();
        public Task Create(StoreMenuRequest menu);
        public Task Update(long Id, UpdateMenuRequest menu);
        public Task<Menu> Show(long Id);
        public List<MenuTree> BuildTree(List<Menu> menus);
        public Task<List<MenuTree>> GetAdminMenu();
        public Task<List<MenuTree>> GetUserMenu();
    }

    // public partial class MenuTree
    // {
    //     public long Id { get; set; }

    //     public string Title { get; set; } = null!;

    //     public string? Url { get; set; }

    //     public string? Icon { get; set; }

    //     public byte Position { get; set; }

    //     public int Lft { get; set; }

    //     public int Rgt { get; set; }

    //     public int? ParentId { get; set; }

    //     public bool? Hidden { get; set; }

    //     public DateTime? CreatedAt { get; set; }

    //     public DateTime? UpdatedAt { get; set; }

    //     public List<MenuTree> Children { get; set; } = new();
    // }
}
