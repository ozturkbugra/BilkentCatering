using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class KurumsalSunumController : Controller
    {
        private readonly ICorporatePresentationService _corporatePresentationService;

        public KurumsalSunumController(ICorporatePresentationService corporatePresentationService)
        {
            _corporatePresentationService = corporatePresentationService;
        }

        [Route("kurumsal-sunum")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Kurumsal Sunum";
            ViewData["BodyClass"] = "starter-page-page";

            var model = _corporatePresentationService.GetSingle();

            return View(model);
        }
    }
}
