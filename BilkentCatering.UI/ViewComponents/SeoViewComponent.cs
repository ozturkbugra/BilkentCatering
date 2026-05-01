using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class SeoViewComponent : ViewComponent
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public SeoViewComponent(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public IViewComponentResult Invoke()
        {
            var model = _siteSettingsService.GetSingle();
            return View(model);
        }
    }
}
