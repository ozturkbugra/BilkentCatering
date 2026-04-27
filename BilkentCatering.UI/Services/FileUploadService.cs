using Microsoft.AspNetCore.Http;

namespace BilkentCatering.UI.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return null;

            var fileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(_env.WebRootPath, "uploads", "images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/uploads/images/" + fileName;
        }

        public string UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".pdf")
                return null;

            var fileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(_env.WebRootPath, "uploads", "pdf");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/uploads/pdf/" + fileName;
        }

        public string UploadVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return null;

            var fileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(_env.WebRootPath, "uploads", "video");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/uploads/video/" + fileName;
        }

        public void DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return;

            var filePath = Path.Combine(_env.WebRootPath, fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}