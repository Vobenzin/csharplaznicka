using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;

        public CartController(ILogger<CartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }
        [HttpGet(Name = "GetCart")]
        public async Task<CartDTO> Get(long userId)
        {
            var cart_dto = await _cartService.GetMyCartAsync(userId);
            return cart_dto;
        }

        [HttpPost(Name = "AddToCart")]
        public async Task<bool> Add(long userId, Guid productPublicId, int amount)
        {
            var action_bool = await _cartService.AddToCartAsync(userId, productPublicId, amount);
            return action_bool;
        }

        [HttpDelete(Name = "RemoveFromCart")]
        public async Task<Boolean> Remove(long userId, Guid cartItemPublicId)
        {
            var action_bool = await _cartService.RemoveItemAsync(userId, cartItemPublicId);
            return action_bool;
        }

        [HttpPut(Name = "UpdateItemAmountInCart")]
        public async Task<Boolean> UpdateAmout(long userId, Guid cartItemPublicId ,int amount)
        {
            var action_bool = await _cartService.UpdateItemAmountAsync(userId,cartItemPublicId, amount);
            return action_bool;
        }

        [HttpDelete(Name = "ClearCart")]
        public async Task<Boolean> Clear(long userId)
        {
            var action_bool = await _cartService.ClearCartAsync(userId);
            return action_bool;
        }

    }
}
