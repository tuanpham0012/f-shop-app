using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Products
{
    public interface IProductRepository
    {
        Task<PaginatedList<ProductVM>> GetAll(ProductRequest request, List<string>? Includes = null!);
        Task<ProductVM> Show(long Id);
        Task Create(StoreProductRequest product);
        Task Update(long id, UpdateProductRequest product);
        Boolean Delete(long id);

        Task<PaginatedList<ProductVM>> GetFeaturedProduct(ProductRequest request);
        Task<PaginatedList<ProductVM>> GetProductByCategory(string categoryCode, ProductRequest request);

    }
}
