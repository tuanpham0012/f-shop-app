using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Metrics;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using System.Diagnostics;
using System;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("products")]
    public class ProductController(IProductRepository _repo) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]ProductRequest request)
        {
            var entries = await _repo.GetAll(request, ["Options", "Options.OptionValues", "Skus", "Skus.Variants", "Category", "Supplier"]);
            
            return Ok(new ResponsePaginatedCollection<Product>(entries));
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreProductRequest product)
        {
            try
            {
                await _repo.Create(product);
                return Ok(new SuccessResponse(200, "Thêm mới thành công"));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
            
        }

        [HttpGet("{Id}")]
        public IActionResult Show(int Id)
        {
            var entry = _repo.Find(Id, ["Options", "Options.OptionValues", "Skus", "Skus.Variants", "Skus.Variants.OptionValue"]);
            if (entry != null)
            {
                try
                {
                    return Ok(new ResponseOne<Product>(entry));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }
    }
}
