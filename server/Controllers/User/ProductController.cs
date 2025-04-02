using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("products")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet("new-product")]
        public async Task<IActionResult> GetNewProduct([FromQuery] ProductRequest request)
        {

            var product = await productRepository.GetAll(request, ["Category"]);
            return Ok();
        }

        [HttpGet("featured-product")]
        public async Task<IActionResult> GetFeaturedProduct([FromQuery] ProductRequest request)
        {
            var product = await productRepository.GetFeaturedProduct(request);
            return Ok(new ResponsePaginatedCollection<ProductVM>(product));
        }

        [Authorize]
        [HttpGet("{CategoryCode}")]
        public async Task<IActionResult> GetProductByCategory(string CategoryCode, [FromQuery] ProductRequest request)
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

        [HttpGet("description/{Alias}")]
        public async Task<IActionResult> GetDesctionProduct(string Alias)
        {
            var product = await productRepository.GetDescriptionProduct(Alias);
            return Ok(new ResponseOne<string>(product ?? ""));
        }

        [HttpGet("sku/{Id}")]
        public async Task<IActionResult> GetSkuProduct(long Id)
        {
            var entry = await productRepository.GetSkuProduct(Id);
            return Ok(new ResponseOne<object>(new
            {
                Options = entry.Options,
                Skus = entry.Skus,
            }));
        }
    }
}