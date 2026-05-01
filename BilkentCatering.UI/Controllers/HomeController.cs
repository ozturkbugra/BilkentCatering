using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly ISiteImageService _siteImageService;
       

        public HomeController(
            ISiteSettingsService siteSettingsService,
            ISiteImageService siteImageService)
        {
            _siteSettingsService = siteSettingsService;
            _siteImageService = siteImageService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Ana Sayfa";
            ViewData["BodyClass"] = "index-page";

            ViewBag.SiteSettings = _siteSettingsService.GetSingle();
            ViewBag.SiteImages = _siteImageService.GetAll().Where(x => !x.IsDeleted).ToList();

            return View();
        }
    }
}