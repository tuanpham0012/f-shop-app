using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryVM>> GetAll(CategoryRequest request);
        List<CategoryTreeVM> BuildTree(List<CategoryVM> categories);
        Task<Category> Create(StoreCategoryRequest request);
        Task Update(long Id, UpdateCategoryRequest request);
        Task<CategoryVM> Show(long Id);
        Task Delete(long Id);

        Task<List<CategoryVM>> GetPopularCategory();

        Task<List<CategoryVM>> GetTopCategoryWithProduct();
        Task<List<CategoryVM>> GetCategoryHasFeaturedProduct();
    }
}
