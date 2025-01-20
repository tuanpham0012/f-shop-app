using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("brands")]
    public class BrandController(IBrandRepository repository) : Controller
    {
        private readonly IBrandRepository _repo = repository;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]BrandRequest request)
        {
            var entries = await _repo.GetAll(request);
            return Ok(new ResponseCollection<Brand>(entries));
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreBrandRequest brand)
        {
            try
            {
                Brand entry = await _repo.Create(brand);
                return Ok(new ResponseOne<Brand>(entry, 200, "Thêm mới thuế sản phẩm thành công!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400,ex.Message));
            }
            
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            try
            {
                var entry = await _repo.Show(Id);
                return Ok(new ResponseOne<Brand>(entry));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400,ex.Message));
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(long Id, UpdateBrandRequest brand)
        {
            try
            {
                await _repo.Update(Id, brand);
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
