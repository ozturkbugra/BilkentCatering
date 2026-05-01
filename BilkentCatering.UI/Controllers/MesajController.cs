using BilkentCatering.Business.Abstract;
using BilkentCatering.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Controllers
{
    public class MesajController : Controller
    {
        private readonly IMessageService _messageService;

        public MesajController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Gonder(Message message)
        {
            if (ModelState.IsValid)
            {
                var result = _messageService.Add(message);

                if (result != null)
                {
                    // Başarılı durumunda JSON dönüyoruz
                    return Json(new { success = true, message = "Mesajınız başarıyla iletildi. En kısa sürede size dönüş yapacağız." });
                }
                else
                {
                    // Servis hatası
                    return Json(new { success = false, message = "Mesaj gönderilirken bir hata oluştu." });
                }
            }

            // Validasyon hatası
            return Json(new { success = false, message = "Lütfen formdaki zorunlu alanları eksiksiz doldurun." });
        }
    }
}

