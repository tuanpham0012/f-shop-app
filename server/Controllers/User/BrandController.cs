using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

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
    }
}