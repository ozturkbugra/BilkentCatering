using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class BelgelerveSertifikalarController : Controller
    {
        private readonly ICertificateAndDocumentService _certificateAndDocumentService;

        public BelgelerveSertifikalarController(ICertificateAndDocumentService certificateAndDocumentService)
        {
            _certificateAndDocumentService = certificateAndDocumentService;
        }

        [Route("belgeler-ve-sertifikalar")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Belgeler ve Sertifikalar";
            ViewData["BodyClass"] = "starter-page-page";

            var model = _certificateAndDocumentService.GetAll();

            return View(model);
        }
    }
}
