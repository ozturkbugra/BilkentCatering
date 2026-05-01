using BilkentCatering.Business.Abstract;
using AdminEntity = BilkentCatering.Entities.Concrete.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Admin Kullanıcıları";
            ViewData["Breadcrumb"] = "Admin Kullanıcıları";
            var admins = _adminService.GetAll().Where(x => !x.IsDeleted).ToList();
            return View(admins);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AdminEntity model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var result = _adminService.Add(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var admin = _adminService.GetById(id);
            if (admin == null) return NotFound();
            return Json(new
            {
                id = admin.Id,
                username = admin.Username,
                fullName = admin.FullName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AdminEntity model, string? newPassword)
        {
            var existing = _adminService.GetById(model.Id);
            if (existing == null)
            {
                TempData["Error"] = "Admin bulunamadı.";
                return RedirectToAction("Index");
            }

            model.Password = !string.IsNullOrEmpty(newPassword)
                ? BCrypt.Net.BCrypt.HashPassword(newPassword)
                : existing.Password;

            var result = _adminService.Update(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var admins = _adminService.GetAll().Where(x => !x.IsDeleted).ToList();
            if (admins.Count <= 1)
            {
                TempData["Error"] = "Son admin kullanıcısı silinemez!";
                return RedirectToAction("Index");
            }

            var admin = _adminService.GetById(id);
            if (admin == null)
            {
                TempData["Error"] = "Admin bulunamadı.";
                return RedirectToAction("Index");
            }

            var result = _adminService.Delete(admin);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}