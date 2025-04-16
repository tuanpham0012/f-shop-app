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
    }
}
