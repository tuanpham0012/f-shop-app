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
        Task<ProductVM> FindProductByAlias(string Alias);
        Task<string?> GetDescriptionProduct(long Id);
        Task<ProductVM> GetSkuProduct(long Id);
        Task<string?> GetDescriptionProduct(string Alias);
        void UpdateDesctionProduct(long Id, ProductDesRequest request);
        Task<List<SkuVM>> SearchProduct(BaseRequest Request);

        void ResetDataElastic();
    }
}
