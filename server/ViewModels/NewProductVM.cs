using ShopAppApi.Data;
using ShopAppApi.Models;

namespace ShopAppApi.ViewModels;
public class NewProductVM
{
    public List<ProductVM> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}