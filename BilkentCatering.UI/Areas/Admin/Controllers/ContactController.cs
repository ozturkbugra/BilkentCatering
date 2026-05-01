using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "İletişim Bilgileri";
            ViewData["Breadcrumb"] = "İletişim Bilgileri";
            var contact = _contactService.GetSingle();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Contact model)
        {
            var existing = _contactService.GetSingle();

            ServiceResult result;

            if (existing == null)
                result = _contactService.Add(model);
            else
            {
                model.Id = existing.Id;
                result = _contactService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}