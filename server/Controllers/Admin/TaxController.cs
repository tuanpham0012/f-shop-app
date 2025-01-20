using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("taxes")]
    public class TaxController(ITaxRepository repository) : Controller
    {
        private readonly ITaxRepository _repo = repository;

        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] TaxRequest request)
        {
            var entries = await _repo.GetAll(request);
            return Ok(new ResponseCollection<Tax>(entries));
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreTaxRequest tax)
        {
            Tax entry = await _repo.Create(tax);
            return Ok(new ResponseOne<Tax>(entry, 200, "Thêm mới thuế sản phẩm thành công!"));
        }
    }
}
