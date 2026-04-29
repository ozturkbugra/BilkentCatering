using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAdminService _adminService;

        public AuthController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["Error"] = "Kullanıcı adı ve şifre boş bırakılamaz.";
                return View();
            }

            var admin = _adminService.GetAll()
                .FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && !x.IsDeleted);

            if (admin == null)
            {
                ViewData["Error"] = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, admin.Password))
            {
                ViewData["Error"] = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.FullName),
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim("Username", admin.Username)
            };

            var identity = new ClaimsIdentity(claims, "AdminCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AdminCookie", principal);

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
        }
    }
}