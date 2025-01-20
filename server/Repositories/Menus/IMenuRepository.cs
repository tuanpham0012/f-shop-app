using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.Menus
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAll();
        Task Create(StoreMenuRequest menu);
        Task Update(long Id, UpdateMenuRequest menu);
        Task<Menu> Show(long Id);
        public List<MenuTree> BuildTree(List<Menu> menus);
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
