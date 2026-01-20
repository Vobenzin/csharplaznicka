using biznis.Interfaces.Services;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

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
            _userService.CreateAdminAsync("admin", "admin@admin.com", "123");
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditForm([FromForm] Guid userPublicId)
        {
            var user = await _userService.GetByPublicIdAsync(userPublicId);
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> User()
        {
            var userList = await _userService.GetAllAsync();
            return View(userList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] string name, [FromForm] string email, [FromForm] string password)
        { 

            await _userService.CreateAsync(name, email, password);
            return RedirectToAction("User");
        }

        [Authorize(Roles = "admin")]
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

        public IActionResult Login() => View();
        public IActionResult Register() => View();

        public IActionResult Forbidden() => View();

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {
            await _userService.CreateAsync(name, email, password);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = await _userService.AuthenticateAsync(email, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login.");
                return View();
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.PublicId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()) // "Admin" or "User"
                };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
