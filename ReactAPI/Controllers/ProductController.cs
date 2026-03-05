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
        private readonly ILogger<UserController> _logger;
        private readonly IProductService _productService;


        //public List<User> UserList { get; set; } = new List<User>();
        public ProductController(ILogger<UserController> logger, IProductService productService)
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

    }
}
