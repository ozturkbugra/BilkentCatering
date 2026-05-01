using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IContactService _contactService;

        public FooterViewComponent(ISiteSettingsService siteSettingsService, IContactService contactService)
        {
            _siteSettingsService = siteSettingsService;
            _contactService = contactService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SiteSettings = _siteSettingsService.GetSingle();
            ViewBag.Contact = _contactService.GetSingle();
            return View();
        }
    }
}