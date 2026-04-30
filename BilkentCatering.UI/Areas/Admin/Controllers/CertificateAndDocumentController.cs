using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class CertificateAndDocumentController : Controller
    {
        private readonly ICertificateAndDocumentService _certificateAndDocumentService;
        private readonly FileUploadService _fileUploadService;

        public CertificateAndDocumentController(ICertificateAndDocumentService certificateAndDocumentService, FileUploadService fileUploadService)
        {
            _certificateAndDocumentService = certificateAndDocumentService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Belgeler & Sertifikalar";
            ViewData["Breadcrumb"] = "Belgeler & Sertifikalar";
            var list = _certificateAndDocumentService.GetAll().Where(x => !x.IsDeleted).ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CertificateAndDocument model, IFormFile? pdfFile)
        {
            if (pdfFile != null)
                model.PdfLink = _fileUploadService.UploadPdf(pdfFile);

            var result = _certificateAndDocumentService.Add(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = _certificateAndDocumentService.GetById(id);
            if (item == null) return NotFound();
            return Json(new
            {
                id = item.Id,
                title = item.Title,
                description = item.Description,
                pdfLink = item.PdfLink
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CertificateAndDocument model, IFormFile? pdfFile)
        {
            var existing = _certificateAndDocumentService.GetById(model.Id);

            if (pdfFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.PdfLink);
                model.PdfLink = _fileUploadService.UploadPdf(pdfFile);
            }
            else
            {
                model.PdfLink = existing?.PdfLink;
            }

            var result = _certificateAndDocumentService.Update(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var item = _certificateAndDocumentService.GetById(id);
            if (item == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction("Index");
            }

            _fileUploadService.DeleteFile(item.PdfLink);
            var result = _certificateAndDocumentService.Delete(item);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}