using biznis.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // User view
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // Admin view
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}
