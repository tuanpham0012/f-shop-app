using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.RepoCustomer;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("customers")]
    public class CustomerController(ICustomerRepository repository) : Controller
    {
        private readonly ICustomerRepository _repo = repository;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CustomerRequest request)
        {
            var entries = await _repo.GetAll(request);
            return Ok(new ResponsePaginatedCollection<Customer>(entries, 201, ""));
        }
        [HttpGet("{Id}")]
        public IActionResult Show(int Id)
        {
            var entry = _repo.Find(Id);

            if (entry != null)
            {
                try
                {
                    return Ok(new ResponseOne<Customer>(entry));
                }
                catch (Exception ex)
                {
                    
                    return BadRequest(new ErrorResponse(422, ex.Message));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreCustomerRequest customer)
        {
            try
            {
                await _repo.Create(customer);
                return Ok(new SuccessResponse(200, "Thêm mới thành cồng!!"));
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateCustomerRequest customer)
        {
            //var id = HttpContext.GetRouteValue("id");
            try
            {
                await _repo.Update(Id, customer);
                return Ok(new SuccessResponse(200, "Cập nhật thành côngg!"));
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Destroy(int Id)
        {
            //var id = HttpContext.GetRouteValue("id");

            var isDeleted = _repo.Delete(Id);
            if (isDeleted)
            {
                return Ok(new { message = "Xoá khách hàng thành công!" });
            }
            return BadRequest();
        }
    }
}
