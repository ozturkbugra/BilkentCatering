using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        private readonly FileUploadService _fileUploadService;

        public AboutUsController(IAboutUsService aboutUsService, FileUploadService fileUploadService)
        {
            _aboutUsService = aboutUsService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Hakkımızda";
            ViewData["Breadcrumb"] = "Hakkımızda";
            var aboutUs = _aboutUsService.GetSingle();
            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(AboutUs model, IFormFile? imageFile)
        {
            var existing = _aboutUsService.GetSingle();

            if (imageFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.ImageUrl);
                model.ImageUrl = _fileUploadService.UploadImage(imageFile);
            }
            else
            {
                model.ImageUrl = existing?.ImageUrl;
            }

            ServiceResult result;

            if (existing == null)
            {
                result = _aboutUsService.Add(model);
            }
            else
            {
                model.Id = existing.Id;
                result = _aboutUsService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}