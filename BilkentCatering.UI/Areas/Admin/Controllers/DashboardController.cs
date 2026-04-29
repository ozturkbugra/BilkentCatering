using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class DashboardController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IServicesService _servicesService;
        private readonly ISiteImageService _siteImageService;

        public DashboardController(
            IMessageService messageService,
            IJobApplicationService jobApplicationService,
            IServicesService servicesService,
            ISiteImageService siteImageService)
        {
            _messageService = messageService;
            _jobApplicationService = jobApplicationService;
            _servicesService = servicesService;
            _siteImageService = siteImageService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            ViewData["Breadcrumb"] = "Dashboard";

            var messages = _messageService.GetAll().ToList();
            var applications = _jobApplicationService.GetAll().ToList();

            ViewBag.TotalMessages = messages.Count();
            ViewBag.UnreadMessages = messages.Count(x => !x.IsRead && !x.IsDeleted);
            ViewBag.TotalApplications = applications.Count();
            ViewBag.UnreadApplications = applications.Count(x => !x.IsRead && !x.IsDeleted);
            ViewBag.TotalServices = _servicesService.GetAll().Count();
            ViewBag.TotalImages = _siteImageService.GetAll().Count();
            ViewBag.RecentMessages = messages.OrderByDescending(x => x.MessageDate).Take(5).ToList();
            ViewBag.RecentApplications = applications.OrderByDescending(x => x.ApplicationDate).Take(5).ToList();

            return View();
        }
    }
}