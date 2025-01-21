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

        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            try
            {
                var entry = await _repo.Show(Id);
                return Ok(new ResponseOne<Tax>(entry));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400,ex.Message));
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(long Id, UpdateTaxRequest request)
        {
            try
            {
                await _repo.Update(Id, request);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Destroy(int Id)
        {
            //var id = HttpContext.GetRouteValue("id");
            try
            {
                await _repo.Delete(Id);
                return Ok(new { message = "Xoá dữ liệu thành công!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }
    }
}
