using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class MusteriMemnuniyetiController : Controller
    {
        private readonly ICustomerSatisfactionService _customerSatisfactionService;

        public MusteriMemnuniyetiController(ICustomerSatisfactionService customerSatisfactionService)
        {
            _customerSatisfactionService = customerSatisfactionService;
        }

        [Route("musteri-memnuniyeti")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Müşteri Memnuniyeti";
            ViewData["BodyClass"] = "starter-page-page";

            var policy = _customerSatisfactionService.GetSingle();

            return View(policy);
        }
    }
}
