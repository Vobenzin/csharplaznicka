using biznis.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private long GetUserId()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return long.Parse(idStr!);
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetMyCartAsync(GetUserId());
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productPublicId, int amount)
        {
            await _cartService.AddToCartAsync(GetUserId(), productPublicId, amount);
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid cartItemPublicId, int amount)
        {
            await _cartService.UpdateItemAmountAsync(GetUserId(), cartItemPublicId, amount);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid cartItemPublicId)
        {
            await _cartService.RemoveItemAsync(GetUserId(), cartItemPublicId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _cartService.ClearCartAsync(GetUserId());
            return RedirectToAction("Index");
        }
    }
}
