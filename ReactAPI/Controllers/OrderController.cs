using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [HttpGet(Name = "GetOrders")]
        public async Task<List<OrderListDTO>> Get(long userId)
        {
            var orders_dto = await _orderService.GetMyOrdersAsync(userId);
            return orders_dto;
        }

        [HttpGet(Name = "GetDetailOrder")]
        public async Task<OrderDetailDTO> GetDetail(long userId, Guid orderPublicId)
        {
            var order_details = await _orderService.GetMyOrderDetailAsync(userId, orderPublicId);
            return order_details;
        }

        [HttpPost(Name = "Checkout")]
        public async Task<CheckoutResultDTO> Checkout(long userId)
        {
            var order_checkout_result = await _orderService.CheckoutAsync(userId);
            return order_checkout_result;
        }


    }
}
