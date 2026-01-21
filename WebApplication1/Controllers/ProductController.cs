using biznis.Interfaces.Services;
using Common.DTO;
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

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // CREATE
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View(new ProductUpsertDTO());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductUpsertDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _productService.CreateAsync(dto);
            return RedirectToAction(nameof(AdminIndex));
        }

        // EDIT
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid publicId)
        {
            var p = await _productService.GetByPublicIdAsync(publicId);
            if (p == null) return NotFound();

            var dto = new ProductUpsertDTO
            {
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Platform = p.Platform,
                Amount = p.Amount,
                Description = p.Description
            };

            ViewBag.PublicId = publicId;
            return View(dto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid publicId, ProductUpsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PublicId = publicId;
                return View(dto);
            }

            await _productService.UpdateAsync(publicId, dto);
            return RedirectToAction(nameof(AdminIndex));
        }

        // DELETE
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid publicId)
        {
            await _productService.DeleteAsync(publicId);
            return RedirectToAction(nameof(AdminIndex));
        }
    }
}
