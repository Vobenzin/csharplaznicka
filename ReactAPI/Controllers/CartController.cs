using Microsoft.AspNetCore.Mvc;

namespace ReactAPI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
