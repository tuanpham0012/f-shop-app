using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Categories;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("admin/categories")]
    public class CategoryController(ICategoryRepository repository) : Controller
    {
        private readonly ICategoryRepository _repo = repository;

        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]CategoryRequest request)
        {
            var entries = await _repo.GetAll(request);
            return Ok(new ResponseCollection<CategoryVM>(entries));
        }

        [HttpGet("get-tree")]
        public async Task<IActionResult> GetTree([FromQuery]CategoryRequest request)
        {
            var entries = await _repo.GetAll(request);
            var tree = _repo.BuildTree(entries);

            return Ok(new ResponseCollection<CategoryTreeVM>(tree));
        }
        [HttpPost]
        public async Task<IActionResult> Store(StoreCategoryRequest request)
        {
            try
            {
                var category = await _repo.Create(request);
                return Ok(new ResponseOne<Category>( category ,  200, "Thêm mới thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            try
            {
                var entry = await _repo.Show(Id);
                return Ok(new ResponseOne<Category>(entry));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400,ex.Message));
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(long Id, UpdateCategoryRequest request)
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
