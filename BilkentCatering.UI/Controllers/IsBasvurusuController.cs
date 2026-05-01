using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class IsBasvurusuController : Controller
    {
        [Route("is-basvurusu")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
