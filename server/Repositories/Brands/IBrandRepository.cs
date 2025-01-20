using ShopAppApi.Data;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAll(BrandRequest request);
        Task<Brand> Create(StoreBrandRequest menu);
        Task Update(long Id, UpdateBrandRequest menu);
        Task<Brand> Show(long Id);
        Task Delete(long Id);
    }
}
