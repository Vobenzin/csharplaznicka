using biznis.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private long GetUserId()
        => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var result = await _orderService.CheckoutAsync(GetUserId());
        return RedirectToAction(nameof(Detail), new { publicId = result.OrderPublicId });
    }

    public async Task<IActionResult> History()
    {
        var list = await _orderService.GetMyOrdersAsync(GetUserId());
        return View(list);
    }

    public async Task<IActionResult> Detail(Guid publicId)
    {
        var order = await _orderService.GetMyOrderDetailAsync(GetUserId(), publicId);
        if (order == null) return NotFound();
        return View(order);
    }
}

