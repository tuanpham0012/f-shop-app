using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("products")]
    public class ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) : Controller
    {
        [HttpGet("new-product")]
        public async Task<IActionResult> GetNewProduct([FromQuery]ProductRequest request)
        {
            request.IsNew = true;
            var product = await productRepository.GetAll(request, ["Category"]);
            return Ok(new ResponsePaginatedCollection<Product>(product));
        }

        [HttpGet("featured-product")]
        public async Task<IActionResult> GetFeaturedProduct([FromQuery]ProductRequest request)
        {
            request.IsFeatured = true;
            var product = await productRepository.GetAll(request, ["Category"]);
            return Ok(new ResponsePaginatedCollection<Product>(product));
        }
    }
}