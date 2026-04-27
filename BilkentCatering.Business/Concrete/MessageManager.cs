using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Message GetById(int id) => _messageRepository.GetById(id);
        public Message GetSingle() => _messageRepository.GetSingle();
        public IEnumerable<Message> GetAll() => _messageRepository.GetAll();

        public ServiceResult Add(Message entity)
        {
            entity.MessageDate = DateTime.Now;
            _messageRepository.Add(entity);
            _messageRepository.Save();
            return ServiceResult.Ok("Mesajınız başarıyla iletildi.");
        }

        public ServiceResult Update(Message entity)
        {
            var existing = _messageRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.IsRead = entity.IsRead;
            existing.UpdatedDate = DateTime.Now;

            _messageRepository.Update(existing);
            _messageRepository.Save();
            return ServiceResult.Ok("Mesaj güncellendi.");
        }

        public ServiceResult Delete(Message entity)
        {
            var existing = _messageRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _messageRepository.Delete(existing);
            _messageRepository.Save();
            return ServiceResult.Ok("Mesaj başarıyla silindi.");
        }
    }
}