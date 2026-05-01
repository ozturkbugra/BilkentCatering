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
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["Error"] = "Kullanıcı adı ve şifre boş bırakılamaz.";
                return View();
            }

            var admin = _adminService.GetAll()
                .FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && !x.IsDeleted);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.Password))
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

            // --- DİNAMİK SÜRE AYARI BURADA YAPILIYOR ---
            var authProperties = new AuthenticationProperties
            {
                // IsPersistent: true olursa tarayıcı kapansa bile cookie silinmez.
                IsPersistent = rememberMe,

                // Hatırla seçildiyse 30 gün, seçilmediyse 1 gün
                ExpiresUtc = rememberMe
                    ? DateTimeOffset.UtcNow.AddMonths(1)
                    : DateTimeOffset.UtcNow.AddDays(1)
            };

            await HttpContext.SignInAsync("AdminCookie", principal, authProperties);

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
        }
    }
}