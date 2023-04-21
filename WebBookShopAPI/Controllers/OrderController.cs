using AutoMapper;
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
    // [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper) {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            var order = await _orderService.CreateOrderAsync(orderDto.ContactName, orderDto.ContactEmail, orderDto.ContactPhone, orderDto.DeviveryId, orderDto.Address, userId, orderDto.BasketId);

            if (order == null) return BadRequest(new ApiResponse(400, "Order creating error"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(userId);
            //return Ok(orders);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetDeliveryMethods()
        {
            var delivery = await _orderService.GetDeliveryMethodsAsync();
            return Ok(_mapper.Map<IReadOnlyList<DeliveryDto>>(delivery));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, userId);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
