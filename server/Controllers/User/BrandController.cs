using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("brands")]
    public class BrandController(IBrandRepository repository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new BrandRequest(){ NotUse = false };
            var entries = await repository.GetAll(request);
            return Ok(new ResponseCollection<Brand>(entries));
        }

        [HttpGet("brand-by-category")]
        public async Task<IActionResult> GetBrandByCategory(string CategoryCode)
        {
            Console.WriteLine(HttpContext.Request.Host.Value);
            var entries = await repository.GetBrandByCategory(CategoryCode);
            return Ok(new ResponseCollection<BrandVM>(entries));
        }
        
    }
}