using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "İş Başvuruları";
            ViewData["Breadcrumb"] = "İş Başvuruları";
            var applications = _jobApplicationService.GetAll().Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.ApplicationDate).ToList();
            return View(applications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleRead(int id)
        {
            var application = _jobApplicationService.GetById(id);
            if (application == null)
            {
                TempData["Error"] = "Başvuru bulunamadı.";
                return RedirectToAction("Index");
            }

            application.IsRead = !application.IsRead;
            _jobApplicationService.Update(application);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var application = _jobApplicationService.GetById(id);
            if (application == null)
            {
                TempData["Error"] = "Başvuru bulunamadı.";
                return RedirectToAction("Index");
            }

            var result = _jobApplicationService.Delete(application);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}