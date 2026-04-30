using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class PolicyController : Controller
    {
        private readonly IPolicyService _policyService;
        private readonly FileUploadService _fileUploadService;

        public PolicyController(IPolicyService policyService, FileUploadService fileUploadService)
        {
            _policyService = policyService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Politikamız";
            ViewData["Breadcrumb"] = "Politikamız";
            var policy = _policyService.GetSingle();
            return View(policy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Policy model, IFormFile? imageFile)
        {
            var existing = _policyService.GetSingle();

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
                result = _policyService.Add(model);
            }
            else
            {
                model.Id = existing.Id;
                result = _policyService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}