using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class TanitimFilmiController : Controller
    {
        private readonly IIntroductionVideoService _introductionVideoService;

        public TanitimFilmiController(IIntroductionVideoService introductionVideoService)
        {
            _introductionVideoService = introductionVideoService;
        }

      /*  [Route("tanitim-filmi")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Tanıtım Filmi";
            ViewData["BodyClass"] = "starter-page-page";

            var model = _introductionVideoService.GetSingle();

            return View(model);
        }*/
    }
}
