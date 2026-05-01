using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class PageTitleViewComponent : ViewComponent
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public PageTitleViewComponent(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var siteSettings = _siteSettingsService.GetSingle();

            // Gelen başlığı View tarafında ekrana basabilmek için ViewBag'e atıyoruz
            ViewBag.PageTitle = title;

            return View(siteSettings);
        }
    }
}