using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Repositories.RepoCustomer;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController(ICustomerRepository repository) : Controller
    {
        [HttpGet("delivery-address/get-list")]
        public async Task<IActionResult> GetDeliveryAddresses()
        {
            if (long.TryParse(User.FindFirstValue("ID"), out var customerId))
            {
                var CustomerId = customerId;
                try
                {
                    var result = await repository.GetDelivery(CustomerId);
                    return Ok(new ResponseCollection<DeliveryAddressVM>(result));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Invalid CustomerId.");

        }
        [HttpPost("delivery-address/create")]
        public IActionResult CreateDelivery([FromBody] DeliveryRequest request)
        {
            try
            {
                if (long.TryParse(User.FindFirstValue("ID"), out var customerId))
                {
                    request.CustomerId = customerId;
                }
                else
                {
                    return BadRequest("Invalid CustomerId.");
                }
                Console.WriteLine($"CustomerId: {request.CustomerId}");
                repository.CreateDelivery(request);
                return Ok(new SuccessResponse(200, "Thêm địa chỉ giao hàng thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delivery-address/update/{id}")]
        public IActionResult UpdateDelivery(long id, [FromBody] DeliveryRequest request)
        {
            try
            {
                repository.UpdateDelivery(id, request);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("delivery-address/edit/{id}")]
        public async Task<IActionResult> GetDelivery(long id)
        {
            try
            {
                var result = await repository.Show(id);
                return Ok(new ResponseOne<DeliveryAddressVM>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}