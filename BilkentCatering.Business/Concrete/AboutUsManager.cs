using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class AboutUsManager : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsManager(IAboutUsRepository aboutUsRepository)
        {
            _aboutUsRepository = aboutUsRepository;
        }

        public AboutUs GetById(int id) => _aboutUsRepository.GetById(id);
        public AboutUs GetSingle() => _aboutUsRepository.GetSingle();
        public IEnumerable<AboutUs> GetAll() => _aboutUsRepository.GetAll();

        public ServiceResult Add(AboutUs entity)
        {
            var existing = _aboutUsRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Hakkımızda kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _aboutUsRepository.Add(entity);
            _aboutUsRepository.Save();
            return ServiceResult.Ok("Hakkımızda bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(AboutUs entity)
        {
            var existing = _aboutUsRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Title = entity.Title;
            existing.Description = entity.Description;
            existing.ImageUrl = entity.ImageUrl;
            existing.ExperienceYear = entity.ExperienceYear;
            existing.UpdatedDate = DateTime.Now;

            _aboutUsRepository.Update(existing);
            _aboutUsRepository.Save();
            return ServiceResult.Ok("Hakkımızda bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(AboutUs entity)
        {
            var existing = _aboutUsRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _aboutUsRepository.Delete(existing);
            _aboutUsRepository.Save();
            return ServiceResult.Ok("Hakkımızda bilgisi başarıyla silindi.");
        }
    }
}