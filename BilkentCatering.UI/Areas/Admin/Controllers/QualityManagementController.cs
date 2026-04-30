using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class QualityManagementController : Controller
    {
        private readonly IQualityManagementService _qualityManagementService;
        private readonly FileUploadService _fileUploadService;

        public QualityManagementController(IQualityManagementService qualityManagementService, FileUploadService fileUploadService)
        {
            _qualityManagementService = qualityManagementService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Kalite Yönetimi";
            ViewData["Breadcrumb"] = "Kalite Yönetimi";
            var quality = _qualityManagementService.GetSingle();
            return View(quality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(QualityManagement model, IFormFile? imageFile)
        {
            var existing = _qualityManagementService.GetSingle();

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
                result = _qualityManagementService.Add(model);
            else
            {
                model.Id = existing.Id;
                result = _qualityManagementService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}