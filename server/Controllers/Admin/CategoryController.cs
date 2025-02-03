using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("categories")]
    public class CategoryController(ICategoryRepository repository) : Controller
    {
        private readonly ICategoryRepository _repo = repository;

        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entries = await _repo.GetAll();
            return Ok(new ResponseCollection<CategoryVM>(entries));
        }

        [HttpGet("get-tree")]
        public async Task<IActionResult> GetTree()
        {
            var entries = await _repo.GetAll();
            var tree = _repo.BuildTree(entries);

            return Ok(new ResponseCollection<CategoryTreeVM>(tree));
        }
        [HttpPost]
        public async Task<IActionResult> Store(StoreCategoryRequest request)
        {
            try
            {
                await _repo.Create(request);
                return Ok(new SuccessResponse(200, "Thêm mới thành công"));
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
                return Ok(new ResponseOne<Menu>(entry));
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
                await _repo.Update(Id, menu);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }
    }
}
