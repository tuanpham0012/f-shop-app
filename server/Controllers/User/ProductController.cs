using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

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
            return Ok();
        }

        [HttpGet("featured-product")]
        public async Task<IActionResult> GetFeaturedProduct([FromQuery]ProductRequest request)
        {
            var product = await productRepository.GetFeaturedProduct(request);
            return Ok(new ResponsePaginatedCollection<ProductVM>(product));
        }

        [HttpGet("{CategoryCode}")]
        public async Task<IActionResult> GetProductByCategory(string CategoryCode, [FromQuery]ProductRequest request)
        {
            var product = await productRepository.GetProductByCategory(CategoryCode, request);
            return Ok(new ResponsePaginatedCollection<ProductVM>(product));
        }

        [HttpGet("find/{Alias}")]
        public async Task<IActionResult> FindProductByAlias(string Alias)
        {
            var product = await productRepository.FindProductByAlias(Alias);
            return Ok(new ResponseOne<ProductVM>(product));
        }
    }
}