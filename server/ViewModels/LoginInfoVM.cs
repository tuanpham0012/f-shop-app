namespace ShopAppApi.ViewModels;
public class LoginInfoVM
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Token { get; set; }

}