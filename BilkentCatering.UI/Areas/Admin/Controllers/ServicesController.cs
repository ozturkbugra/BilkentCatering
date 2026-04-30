using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly FileUploadService _fileUploadService;

        public ServicesController(IServicesService servicesService, FileUploadService fileUploadService)
        {
            _servicesService = servicesService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Hizmetler";
            ViewData["Breadcrumb"] = "Hizmetler";
            var services = _servicesService.GetAll().Where(x => !x.IsDeleted).ToList();
            return View(services);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Service model, IFormFile? imageFile)
        {
            if (imageFile != null)
                model.ImageUrl = _fileUploadService.UploadImage(imageFile);

            var result = _servicesService.Add(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var service = _servicesService.GetById(id);
            if (service == null) return NotFound();
            return Json(new
            {
                id = service.Id,
                title = service.Title,
                description = service.Description,
                imageUrl = service.ImageUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Service model, IFormFile? imageFile)
        {
            var existing = _servicesService.GetById(model.Id);

            if (imageFile != null)
            {
                if (existing != null) _fileUploadService.DeleteFile(existing.ImageUrl);
                model.ImageUrl = _fileUploadService.UploadImage(imageFile);
            }
            else
            {
                model.ImageUrl = existing?.ImageUrl;
            }

            var result = _servicesService.Update(model);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var service = _servicesService.GetById(id);
            if (service == null)
            {
                TempData["Error"] = "Hizmet bulunamadı.";
                return RedirectToAction("Index");
            }

            _fileUploadService.DeleteFile(service.ImageUrl);
            var result = _servicesService.Delete(service);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}