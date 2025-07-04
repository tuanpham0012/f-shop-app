﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            var entry = await repository.Show(Id);
            return Ok(new ResponseOne<OrderVM>(entry));
        }

        [HttpGet("order-details/{Id}")]
        public async Task<IActionResult> GetOrderDetail(long Id)
        {
            var entry = await repository.GetOrderDetails(Id);
            return Ok(new ResponseCollection<OrderDetailVM>(entry));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(StoreOrderRequest request)
        {
            try
            {
                await repository.Create(request);
                return Ok(new SuccessResponse(200, "Thêm mới thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }
        [HttpPost("change-order-status")]
        public IActionResult ChangeOrderStatus(ChangeOrderStatusRequest request)
        {
            try
            {
                repository.ChangeOrderStatus(request);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }
    }
}
