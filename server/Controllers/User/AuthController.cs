using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Auth;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthRepository repository) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await repository.Login(request);
                return Ok(new ResponseOne<LoginInfoVM>(token));
            }
            catch (Exception ex)
            {
                
                return Unauthorized(ex.Message);
            }
            
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> Info()
        {
            long CustomerId = 0;
            if (long.TryParse(User.FindFirstValue("ID"), out var customerId))
            {
                CustomerId = customerId;
            }
            else
            {
                return BadRequest("Invalid CustomerId.");
            }
            try
            {
                
                var token = await repository.GetInfo(CustomerId);
                return Ok(new ResponseOne<LoginInfoVM>(token));
            }
            catch (Exception ex)
            {
                
                return Unauthorized(ex.Message);
            }
            
        }
        
    }
}