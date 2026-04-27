using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Contact GetById(int id) => _contactRepository.GetById(id);
        public Contact GetSingle() => _contactRepository.GetSingle();
        public IEnumerable<Contact> GetAll() => _contactRepository.GetAll();

        public ServiceResult Add(Contact entity)
        {
            var existing = _contactRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("İletişim kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _contactRepository.Add(entity);
            _contactRepository.Save();
            return ServiceResult.Ok("İletişim bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(Contact entity)
        {
            var existing = _contactRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.MapLink = entity.MapLink;
            existing.Address = entity.Address;
            existing.Email = entity.Email;
            existing.Phone = entity.Phone;
            existing.WhatsApp = entity.WhatsApp;
            existing.Instagram = entity.Instagram;
            existing.UpdatedDate = DateTime.Now;

            _contactRepository.Update(existing);
            _contactRepository.Save();
            return ServiceResult.Ok("İletişim bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(Contact entity)
        {
            var existing = _contactRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _contactRepository.Delete(existing);
            _contactRepository.Save();
            return ServiceResult.Ok("İletişim bilgisi başarıyla silindi.");
        }
    }
}