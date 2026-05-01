using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class CorporatePresentationController : Controller
    {
        private readonly ICorporatePresentationService _corporatePresentationService;
        private readonly FileUploadService _fileUploadService;

        public CorporatePresentationController(ICorporatePresentationService corporatePresentationService, FileUploadService fileUploadService)
        {
            _corporatePresentationService = corporatePresentationService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Kurumsal Sunum";
            ViewData["Breadcrumb"] = "Kurumsal Sunum";
            var presentation = _corporatePresentationService.GetSingle();
            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CorporatePresentation model, IFormFile? pdfFile)
        {
            var existing = _corporatePresentationService.GetSingle();

            if (pdfFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.PdfLink);
                model.PdfLink = _fileUploadService.UploadPdf(pdfFile);
            }
            else
            {
                model.PdfLink = existing?.PdfLink;
            }

            ServiceResult result;

            if (existing == null)
                result = _corporatePresentationService.Add(model);
            else
            {
                model.Id = existing.Id;
                result = _corporatePresentationService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}