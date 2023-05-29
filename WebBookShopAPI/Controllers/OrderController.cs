using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBookShopAPI.Data;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Helpers;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models.OrderEntities;
using WebBookShopAPI.Extensions;
using WebBookShopAPI.Migrations;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public OrderController(IOrderService orderService, IMapper mapper, AppDbContext context) {
            _orderService = orderService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            var order = await _orderService.CreateOrderAsync(orderDto.ContactName, orderDto.ContactEmail, orderDto.ContactPhone, orderDto.DeviveryId, orderDto.Address, userId, orderDto.BasketId);

            if (order == null) return BadRequest(new ApiResponse(400, "Order creating error"));

            return Ok(order);
        }

        [HttpPut("change-order-status")]
        public async Task<ActionResult<OrderToReturnDto>> ChangeOrderStatusAsync(int orderId, int orderStatusId)
        {
            var response = await _orderService.ChangeOrderStatusAsync(orderId, orderStatusId);
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (response) return _mapper.Map<OrderToReturnDto>(order);
            else return BadRequest("Status Update ERROR");
        }

        [HttpGet("get-orders-for-current-user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser([FromQuery] PaginationParams pagParams)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(userId);
            var totalItems = orders.Count();

            var data = orders
                .Skip((pagParams.PageIndex - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToList();

            var orderList = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(data);

            return Ok(new Pagination<OrderToReturnDto>(pagParams.PageIndex, pagParams.PageSize, totalItems, orderList));
        }

        [HttpGet("get-orders-for-admin")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForAdmin([FromQuery] PaginationParams pagParams)
        {
            var orders = await _orderService.GetOrdersForAdminAsync();
            var totalItems = orders.Count();

            var data = orders
                .Skip((pagParams.PageIndex - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToList();

            var orderList = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(data);

            return Ok(new Pagination<OrderToReturnDto>(pagParams.PageIndex, pagParams.PageSize, totalItems, orderList));

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

            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
