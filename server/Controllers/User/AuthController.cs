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

        // Endpoint để bắt đầu quá trình đăng nhập với Google
        // Client sẽ gọi: GET /api/auth/login-google
        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            // Thuộc tính AuthenticationProperties cho phép chúng ta cấu hình thêm cho quá trình xác thực
            // Ở đây, ta chỉ định rằng sau khi xác thực thành công, người dùng sẽ được chuyển hướng đến
            // endpoint /api/auth/google-callback
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback", "Auth") };
            Console.WriteLine("Redirecting to Google for authentication...");
            // ChallengeSchene sẽ kích hoạt handler của Google, điều hướng người dùng đến Google
            try
            {
                return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Google authentication: {ex.Message}");
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }

        }

        // Endpoint này là callback sau khi Google xác thực thành công.
        // Nó sẽ được gọi bởi middleware của .NET chứ không phải người dùng trực tiếp.
        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            Console.WriteLine("Lấy kết quả xác thực từ cookie tạm thời");
            // Lấy kết quả xác thực từ cookie tạm thời
            var result = await HttpContext.AuthenticateAsync();
            if (!result.Succeeded)
                return BadRequest();

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Bạn có thể tạo JWT hoặc lưu vào DB ở đây
            return Ok(new { Email = email });
            // =========================================================================
        }
    }
}