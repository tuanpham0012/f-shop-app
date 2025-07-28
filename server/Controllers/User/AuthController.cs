using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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

        [HttpPost("login-google")]
        public async Task<IActionResult> LoginGoogle([FromBody] LoginGoogleRequest request)
        {
            var payload = await repository.LoginWithGoogle(request);
            if (payload == null)
            {
                return BadRequest("Invalid Google token.");
            }

            // Tạo cookie cho người dùng
            return Ok(new ResponseOne<LoginInfoVM>(payload));
        }

    }
}