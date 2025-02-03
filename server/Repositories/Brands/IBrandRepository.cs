using ShopAppApi.Data;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAll(BrandRequest request);
        Task<Brand> Create(StoreBrandRequest request);
        Task Update(long Id, UpdateBrandRequest request);
        Task<Brand> Show(long Id);
        Task Delete(long Id);
    }
}
