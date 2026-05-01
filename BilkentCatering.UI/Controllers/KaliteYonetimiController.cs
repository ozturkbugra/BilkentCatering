using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class KaliteYonetimiController : Controller
    {
        private readonly IQualityManagementService _qualityManagementService;

        public KaliteYonetimiController(IQualityManagementService qualityManagementService)
        {
            _qualityManagementService = qualityManagementService;
        }

        [Route("kalite-yonetimi")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Kalite Yönetimi";
            ViewData["BodyClass"] = "starter-page-page";

            var policy = _qualityManagementService.GetSingle();

            return View(policy);
        }
    }
}
