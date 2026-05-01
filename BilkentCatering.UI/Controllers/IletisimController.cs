using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class IletisimController : Controller
    {
        [Route("iletisim")]
        public IActionResult Index()
        {
            ViewData["Title"] = "İletişim";
            ViewData["BodyClass"] = "starter-page-page";
            return View();
        }
    }
}
