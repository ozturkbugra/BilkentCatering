using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class HakkimizdaController : Controller
    {
        [Route("hakkimizda")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Hakkımızda";
            ViewData["BodyClass"] = "starter-page-page";
            return View();
        }
    }
}
