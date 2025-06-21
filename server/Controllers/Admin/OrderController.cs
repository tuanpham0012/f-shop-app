using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Orders;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("admin/orders")]
    public class OrderController(IOrderRepository repository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] OrderRequest request)
        {
            var entries = await repository.GetAll(request);
            return Ok(new ResponsePaginatedCollection<OrderVM>(entries));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            var entry = await repository.Show(Id);
            return Ok(new ResponseOne<OrderVM>(entry));
        }

        [HttpGet("order-details/{Id}")]
        public async Task<IActionResult> GetOrderDetail(long Id)
        {
            var entry = await repository.GetOrderDetails(Id);
            return Ok(new ResponseCollection<OrderDetailVM>(entry));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest(new ErrorResponse(400, "Invalid CustomerId."));
            }
            var entry = await repository.CreateOrder(request);
            return Ok(new ResponseOne<OrderVM>(entry));
        }
    }
}
