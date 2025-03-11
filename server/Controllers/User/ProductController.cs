using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("products")]
    public class ProductController(IProductRepository productRepository) : Controller
    {
        [HttpGet("new-product")]
        public async Task<IActionResult> GetNewProduct([FromQuery]ProductRequest request)
        {
            
            var product = await productRepository.GetAll(request, ["Category"]);
            return Ok(new ResponsePaginatedCollection<Product>(product));
        }

        [HttpGet("featured-product")]
        public async Task<IActionResult> GetFeaturedProduct([FromQuery]ProductRequest request)
        {
            var product = await productRepository.GetFeaturedProduct(request, ["Category"]);
            return Ok(new ResponsePaginatedCollection<Product>(product));
        }

        [HttpGet("{CategoryCode}")]
        public async Task<IActionResult> GetProductByCategory(string CategoryCode, [FromQuery]ProductRequest request)
        {
            var product = await productRepository.GetProductByCategory(CategoryCode, request, ["Category"]);
            return Ok(new ResponsePaginatedCollection<Product>(product));
        }
    }
}