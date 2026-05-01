using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class PolitikamizController : Controller
    {
        private readonly IPolicyService _policyService;

        public PolitikamizController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [Route("politikamiz")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Politikamız";
            ViewData["BodyClass"] = "starter-page-page";

            var policy = _policyService.GetSingle();

            return View(policy);
        }
    }
}
