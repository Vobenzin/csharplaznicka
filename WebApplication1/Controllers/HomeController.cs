using biznis.Interfaces.Services;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext db;
        private readonly IUserService _userService;


        public HomeController(ILogger<HomeController> logger, AppDbContext context, IUserService userService)
        {
            _logger = logger;
            UserEntity user = new UserEntity();
            db = context;
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditForm([FromForm] Guid userPublicId)
        {
            var user = await _userService.GetByPublicIdAsync(userPublicId);
            return View(user);
        }


        public async Task<IActionResult> User()
        {
            var userList = await _userService.GetAllAsync();
            return View(userList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] string name, [FromForm] string email)
        { 

            await _userService.CreateAsync(name, email);
            return RedirectToAction("User");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid userPublicId)
        {
            await _userService.DeleteAsync(userPublicId);
            return RedirectToAction("User");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Guid userPublicId, [FromForm] string name, [FromForm] string email)
        {
            await _userService.UpdateAsync(userPublicId, name, email);
            return RedirectToAction("User");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
