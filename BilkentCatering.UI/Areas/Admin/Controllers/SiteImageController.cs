using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using BilkentCatering.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class SiteImageController : Controller
    {
        private readonly ISiteImageService _siteImageService;
        private readonly FileUploadService _fileUploadService;

        public SiteImageController(ISiteImageService siteImageService, FileUploadService fileUploadService)
        {
            _siteImageService = siteImageService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Site Resimleri";
            ViewData["Breadcrumb"] = "Site Resimleri";
            var images = _siteImageService.GetAll().Where(x => !x.IsDeleted).ToList();
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                TempData["Error"] = "Lütfen bir resim seçin.";
                return RedirectToAction("Index");
            }

            var imageUrl = _fileUploadService.UploadImage(imageFile);

            if (imageUrl == null)
            {
                TempData["Error"] = "Geçersiz dosya formatı. JPG, JPEG, PNG veya WebP yükleyin.";
                return RedirectToAction("Index");
            }

            var result = _siteImageService.Add(new SiteImage { ImageUrl = imageUrl });
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var image = _siteImageService.GetById(id);
            if (image == null)
            {
                TempData["Error"] = "Resim bulunamadı.";
                return RedirectToAction("Index");
            }

            _fileUploadService.DeleteFile(image.ImageUrl);
            var result = _siteImageService.Delete(image);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}