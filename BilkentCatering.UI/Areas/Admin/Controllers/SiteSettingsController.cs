using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class SiteSettingsController : Controller
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly FileUploadService _fileUploadService;

        public SiteSettingsController(ISiteSettingsService siteSettingsService, FileUploadService fileUploadService)
        {
            _siteSettingsService = siteSettingsService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Site Ayarları";
            ViewData["Breadcrumb"] = "Site Ayarları";

            var settings = _siteSettingsService.GetSingle();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(SiteSettings model, IFormFile? logoFile, IFormFile? iconFile, IFormFile? largeIconFile)
        {
            var existing = _siteSettingsService.GetSingle();

            if (logoFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.Logo);
                model.Logo = _fileUploadService.UploadImage(logoFile);
            }
            else
            {
                model.Logo = existing?.Logo;
            }

            if (iconFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.Icon);
                model.Icon = _fileUploadService.UploadImage(iconFile);
            }
            else
            {
                model.Icon = existing?.Icon;
            }

            if (largeIconFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.LargeIcon);
                model.LargeIcon = _fileUploadService.UploadImage(largeIconFile);
            }
            else
            {
                model.LargeIcon = existing?.LargeIcon;
            }

            ServiceResult result;

            if (existing == null)
            {
                result = _siteSettingsService.Add(model);
            }
            else
            {
                model.Id = existing.Id;
                result = _siteSettingsService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}