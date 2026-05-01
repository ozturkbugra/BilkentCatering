using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.ViewComponents
{
    public class CounterViewComponent : ViewComponent
    {
        private readonly ICounterService _counterService;

        public CounterViewComponent(ICounterService counterService)
        {
            _counterService = counterService;
        }

        public IViewComponentResult Invoke()
        {
            var counter = _counterService.GetSingle();
            return View(counter);
        }
    }
}