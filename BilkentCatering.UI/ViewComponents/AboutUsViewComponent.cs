using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class AboutUsViewComponent : ViewComponent
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsViewComponent(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public IViewComponentResult Invoke()
        {
            var aboutUs = _aboutUsService.GetSingle();
            return View(aboutUs);
        }
    }
}