using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class CounterController : Controller
    {
        private readonly ICounterService _counterService;

        public CounterController(ICounterService counterService)
        {
            _counterService = counterService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Sayaç";
            ViewData["Breadcrumb"] = "Sayaç";
            var counter = _counterService.GetSingle();
            return View(counter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Counter model)
        {
            var existing = _counterService.GetSingle();

            ServiceResult result;

            if (existing == null)
            {
                result = _counterService.Add(model);
            }
            else
            {
                model.Id = existing.Id;
                result = _counterService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}