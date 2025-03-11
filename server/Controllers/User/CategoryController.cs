using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Categories;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("categories")]
    public class CategoryController(ICategoryRepository repository) : Controller
    {
        [HttpGet("popular-category")]
        public async Task<IActionResult> GetPopularCategory()
        {
            var entries = await repository.GetPopularCategory();
            return Ok(new ResponseCollection<Category>(entries));
        }

        [HttpGet("top-category")]
        public async Task<IActionResult> GetTopCategoryWithProduct()
        {
            var entries = await repository.GetTopCategoryWithProduct();
            return Ok(new ResponseCollection<Category>(entries));
        }

        [HttpGet("featured-product-category")]
        public async Task<IActionResult> GetCategoryHasFeaturedProduct()
        {
            var entries = await repository.GetCategoryHasFeaturedProduct();
            return Ok(new ResponseCollection<Category>(entries));
        }
    }
}