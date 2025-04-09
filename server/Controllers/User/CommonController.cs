using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Common;
using ShopAppApi.Response;

namespace ShopAppApi.Controllers.User
{
    [ApiController]
    [Route("master-data")]
    public class CommonController(ICommonRepository repository) : Controller
    {
        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetPaymentMethod()
        {
            try
            {
                var entries = await repository.PaymentMethods();
                return Ok(new ResponseCollection<PaymentMethod>(entries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpGet("shipping-units")]
        public async Task<IActionResult> GetShippingUnit()
        {
            try
            {
                var entries = await repository.ShippingUnits();
                return Ok(new ResponseCollection<ShippingUnit>(entries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}