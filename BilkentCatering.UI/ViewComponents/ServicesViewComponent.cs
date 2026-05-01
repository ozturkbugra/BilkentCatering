using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IServicesService _servicesService;

        public ServicesViewComponent(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public IViewComponentResult Invoke()
        {
            var services = _servicesService.GetAll().Where(x => !x.IsDeleted).ToList();
            return View(services);
        }
    }
}