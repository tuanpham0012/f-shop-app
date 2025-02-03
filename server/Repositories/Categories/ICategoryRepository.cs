using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.Products
{
    public interface ICategoryRepository
    {
        Task<List<CategoryVM>> GetAll();
        List<CategoryTreeVM> BuildTree(List<CategoryVM> categories);
        Task<Brand> Create(StoreCategoryRequest request);
        Task Update(long Id, UpdateCategoryRequest request);
        Task<Brand> Show(long Id);
        Task Delete(long Id);
    }
}
