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
        
    }
}