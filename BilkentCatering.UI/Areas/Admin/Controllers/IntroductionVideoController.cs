using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class IntroductionVideoController : Controller
    {
        private readonly IIntroductionVideoService _introductionVideoService;

        public IntroductionVideoController(IIntroductionVideoService introductionVideoService)
        {
            _introductionVideoService = introductionVideoService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Tanıtım Filmi";
            ViewData["Breadcrumb"] = "Tanıtım Filmi";
            var video = _introductionVideoService.GetSingle();
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(IntroductionVideo model)
        {
            var existing = _introductionVideoService.GetSingle();

            ServiceResult result;

            if (existing == null)
                result = _introductionVideoService.Add(model);
            else
            {
                model.Id = existing.Id;
                result = _introductionVideoService.Update(model);
            }

            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}