using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class HizmetlerimizController : Controller
    {
        private readonly IServicesService _servicesService;

        public HizmetlerimizController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [Route("hizmetlerimiz")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Hizmetlerimiz";
            ViewData["BodyClass"] = "starter-page-page";

            var model = _servicesService.GetSingle();

            return View(model);
        }
    }
}
