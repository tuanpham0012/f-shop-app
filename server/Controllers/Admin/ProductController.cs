using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("admin/products")]
    public class ProductController(IProductRepository _repo) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ProductRequest request)
        {
            var entries = await _repo.GetAll(request, ["Options", "Options.OptionValues", "Skus", "Skus.Variants", "Category", "Brand", "ProductImages"]);

            return Ok(new ResponsePaginatedCollection<ProductVM>(entries));
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreProductRequest product)
        {
            try
            {
                await _repo.Create(product);
                return Ok(new SuccessResponse(200, "Thêm mới thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(long Id, UpdateProductRequest product)
        {
            try
            {
                await _repo.Update(Id, product);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }


        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            try
            {
                var entry = await _repo.Show(Id);
                return Ok(new ResponseOne<ProductVM>(entry));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("description/{Id}")]
        public async Task<IActionResult> GetDesctionProduct(long Id)
        {
            var product = await _repo.GetDescriptionProduct(Id);
            return Ok(new ResponseOne<string>(product ?? ""));
        }

        [HttpPut("description/{Id}")]
        public IActionResult UpdateDesctionProduct(long Id, ProductDesRequest request)
        {
            _repo.UpdateDesctionProduct(Id, request);
            return Ok(new SuccessResponse(200, "Cập nhật thành công"));
        }

        [HttpGet("sku/{Id}")]
        public async Task<IActionResult> GetSkuProduct(long Id)
        {
            var entry = await _repo.GetSkuProduct(Id);
            return Ok(new ResponseOne<object>(new{
                Options = entry.Options,
                Skus = entry.Skus,
            }));
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery]BaseRequest Request)
        {
            try
            {
                var entry = await _repo.SearchProduct(Request);
                return Ok(new ResponseCollection<SkuVM>(entry));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("reset-data-elastic")]
        public IActionResult ResetDataElastic()
        {
            try
            {
                _repo.ResetDataElastic();
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
