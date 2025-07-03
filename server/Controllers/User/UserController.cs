using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Auth;
using ShopAppApi.Repositories.Products;
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
        [HttpGet("delivery/get")]
        public async Task<IActionResult> GetDelivery()
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
        [HttpPost("delivery/create")]
        public async Task<IActionResult> CreateDelivery([FromBody] DeliveryRequest request)
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
                var result = await repository.CreateDelivery(request);
                return Ok(new ResponseOne<DeliveryAddress>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("delivery/{id}")]
        public async Task<IActionResult> UpdateDelivery(long id, [FromBody] DeliveryRequest request)
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
                var result = await repository.UpdateDelivery(customerId, request);
                return Ok(new ResponseOne<DeliveryAddress>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}