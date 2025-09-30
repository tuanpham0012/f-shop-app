namespace ShopAppApi.ViewModels;
public partial class MenuTree
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public byte Position { get; set; }
    public int? ParentId { get; set; }
    public bool? Active { get; set; }
    public bool? GroupMenu { get; set; }
    public List<MenuTree> Children { get; set; } = new();
}