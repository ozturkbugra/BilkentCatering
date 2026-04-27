using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class SiteSettingsManager : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _siteSettingsRepository;

        public SiteSettingsManager(ISiteSettingsRepository siteSettingsRepository)
        {
            _siteSettingsRepository = siteSettingsRepository;
        }

        public SiteSettings GetById(int id) => _siteSettingsRepository.GetById(id);
        public SiteSettings GetSingle() => _siteSettingsRepository.GetSingle();
        public IEnumerable<SiteSettings> GetAll() => _siteSettingsRepository.GetAll();

        public ServiceResult Add(SiteSettings entity)
        {
            var existing = _siteSettingsRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Site ayarları zaten mevcut. Yeni kayıt eklenemez.");

            _siteSettingsRepository.Add(entity);
            _siteSettingsRepository.Save();
            return ServiceResult.Ok("Site ayarları başarıyla eklendi.");
        }

        public ServiceResult Update(SiteSettings entity)
        {
            var existing = _siteSettingsRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Slogan = entity.Slogan;
            existing.Description = entity.Description;
            existing.Logo = entity.Logo;
            existing.CompanyName = entity.CompanyName;
            existing.Icon = entity.Icon;
            existing.LargeIcon = entity.LargeIcon;
            existing.Title = entity.Title;
            existing.MetaDescription = entity.MetaDescription;
            existing.MetaKeyword = entity.MetaKeyword;
            existing.IsLogoVisible = entity.IsLogoVisible;
            existing.IsCompanyNameVisible = entity.IsCompanyNameVisible;
            existing.UpdatedDate = DateTime.Now;

            _siteSettingsRepository.Update(existing);
            _siteSettingsRepository.Save();
            return ServiceResult.Ok("Site ayarları başarıyla güncellendi.");
        }

        public ServiceResult Delete(SiteSettings entity)
        {
            var existing = _siteSettingsRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _siteSettingsRepository.Delete(existing);
            _siteSettingsRepository.Save();
            return ServiceResult.Ok("Site ayarları başarıyla silindi.");
        }
    }
}