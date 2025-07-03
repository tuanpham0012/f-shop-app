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

        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            try
            {
                var entries = await repository.Provinces();
                return Ok(new ResponseCollection<Province>(entries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("wards/{provinceId}")]
        public async Task<IActionResult> GetWards(long provinceId)
        {
            try
            {
                var entries = await repository.Wards(provinceId);
                return Ok(new ResponseCollection<Ward>(entries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("server-name")]
        public async Task<IActionResult> GetSQLServerName()
        {
            try
            {
                var entries = await repository.GetServerName();
                Console.WriteLine(entries);
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}