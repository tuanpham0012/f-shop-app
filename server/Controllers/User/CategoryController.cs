using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
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
    }
}