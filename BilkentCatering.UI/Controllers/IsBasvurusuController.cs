using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class IsBasvurusuController : Controller
    {
        private readonly IJobApplicationService _jobApplicationService;

        public IsBasvurusuController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [Route("is-basvurusu")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("IsBasvurusu/BasvuruYap")]
        [ValidateAntiForgeryToken]
        public IActionResult BasvuruYap(JobApplication application)
        {
            if (ModelState.IsValid)
            {

                string honeypot = Request.Form["WebsiteUrl"];
                if (!string.IsNullOrEmpty(honeypot))
                {
                    // Bot yakalandı!
                    // Bota hata döndürmüyoruz ki anladığımızı çakmasın. "Başarılı" dönüyoruz ama veritabanına KAYDETMİYORUZ.
                    return Json(new { success = true, message = "Mesajınız başarıyla iletildi. En kısa sürede size dönüş yapacağız." });
                }

                var result = _jobApplicationService.Add(application);

                if (result != null)
                {
                    return Json(new { success = true, message = "Başvurunuz başarıyla alındı. İnsan kaynakları departmanımız sizinle iletişime geçecektir." });
                }
                else
                {
                    return Json(new { success = false, message = "Başvuru sırasında bir hata oluştu." });
                }
            }

            return Json(new { success = false, message = "Lütfen formdaki zorunlu alanları eksiksiz doldurun." });
        }
    }
}