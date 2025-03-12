using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Products
{
    public interface IBrandRepository
    {
        Task<List<BrandVM>> GetAll(BrandRequest request);
        Task<Brand> Create(StoreBrandRequest request);
        Task Update(long Id, UpdateBrandRequest request);
        Task<Brand> Show(long Id);
        Task Delete(long Id);

        Task<List<BrandVM>> GetBrandByCategory(string CategoryCode);

    }
}
