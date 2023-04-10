using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models.OrderEntities;
using WebBookShopAPI.Extensions;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            /*string ContactEmail, string ContactPhone, int deliveryId, string Address, string UserId, string basketId*/

            var order = await _orderService.CreateOrderAsync(orderDto.ContactEmail, orderDto.ContactPhone, orderDto.DeviveryId, orderDto.Address, userId, orderDto.BasketId);

            if (order == null) return BadRequest(new ApiResponse(400, "Order creating error"));

            return Ok(order);
        }
    }
}
