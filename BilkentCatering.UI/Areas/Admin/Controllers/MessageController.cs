using BilkentCatering.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilkentCatering.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Mesajlar";
            ViewData["Breadcrumb"] = "Mesajlar";
            var messages = _messageService.GetAll().Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.MessageDate).ToList();
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleRead(int id)
        {
            var message = _messageService.GetById(id);
            if (message == null)
            {
                TempData["Error"] = "Mesaj bulunamadı.";
                return RedirectToAction("Index");
            }

            message.IsRead = !message.IsRead;
            _messageService.Update(message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = _messageService.GetById(id);
            if (message == null)
            {
                TempData["Error"] = "Mesaj bulunamadı.";
                return RedirectToAction("Index");
            }

            var result = _messageService.Delete(message);
            TempData[result.Success ? "Success" : "Error"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}