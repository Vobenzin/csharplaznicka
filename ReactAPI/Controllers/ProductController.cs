using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet(Name = "GetProducts")]
        public async Task<List<ProductListDTO>> Get()
        {
            var productList = await _productService.GetAllAsync();
            return productList;
        }

        [HttpGet(Name = "GetProductById")]
        public async Task<ProductDTO> GetByPublicId(Guid publicId)
        {
            var productList = await _productService.GetByPublicIdAsync(publicId);
            return productList;
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<Boolean> Create(ProductUpsertDTO dto)
        {
            var action_bool = await _productService.CreateAsync(dto);
            return action_bool;
        }

        [HttpPut(Name = "UpdateProduct")]
        public async Task<Boolean> Update(Guid publicId, ProductUpsertDTO dto)
        {
            var action_bool = await _productService.UpdateAsync(publicId, dto);
            return action_bool;
        }

        [HttpDelete(Name = "DeleteProduct")]
        public async Task<Boolean> Delete(Guid publicId)
        {
            var action_bool = await _productService.DeleteAsync(publicId);
            return action_bool;
        }

    }
}
