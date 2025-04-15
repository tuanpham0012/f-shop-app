using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using ShopAppApi.Data;
using ShopAppApi.Repositories.CartRepo;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("cart")]
    [Authorize]
    public class CartController(ICartRepository repository) : Controller
    {
        [HttpPost]
        public IActionResult AddToCart(AddCartRequest request)
        {
            if (long.TryParse(User.FindFirstValue("ID"), out var customerId))
            {
                request.CustomerId = customerId;
            }
            else
            {
                return BadRequest("Invalid CustomerId.");
            }
            repository.AddToCart(request);
            return Ok(new SuccessResponse(200, "Thêm vào giỏ hàng thành công!"));
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateCart(long Id, UpdateCartRequest request)
        {
            repository.UpdateCart(Id, request);
            return Ok(new SuccessResponse(200, "Cập nhật giỏ hàng thành công!"));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteCart(long Id)
        {
            repository.DeleteCart(Id);
            return Ok(new SuccessResponse(200, "Xoá giỏ hàng thành công!"));
        }

        [HttpDelete("delete-all")]
        public IActionResult DeleteAllCart([FromBody] DeleteCartRequest request)
        {
            repository.DeleteAllCart(request);
            return Ok(new SuccessResponse(200, "Xoá giỏ hàng thành công!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
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
            var entries = await repository.GetCart(CustomerId);
            return Ok(new ResponseCollection<CartVM>(entries));
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CheckoutRequest request)
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
                repository.Checkout(CustomerId, request);
                return Ok(new SuccessResponse(200, "Đặt hàng thành công!"));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        
    }
}