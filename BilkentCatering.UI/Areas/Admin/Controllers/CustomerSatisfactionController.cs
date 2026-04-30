using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class CustomerSatisfactionController : Controller
    {
        private readonly ICustomerSatisfactionService _customerSatisfactionService;
        private readonly FileUploadService _fileUploadService;

        public CustomerSatisfactionController(ICustomerSatisfactionService customerSatisfactionService, FileUploadService fileUploadService)
        {
            _customerSatisfactionService = customerSatisfactionService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Müşteri Memnuniyeti";
            ViewData["Breadcrumb"] = "Müşteri Memnuniyeti";
            var customerSatisfaction = _customerSatisfactionService.GetSingle();
            return View(customerSatisfaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CustomerSatisfaction model, IFormFile? imageFile)
        {
            var existing = _customerSatisfactionService.GetSingle();

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
                result = _customerSatisfactionService.Add(model);
            else
            {
                model.Id = existing.Id;
                result = _customerSatisfactionService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}