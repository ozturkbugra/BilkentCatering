using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class SiteImageManager : ISiteImageService
    {
        private readonly ISiteImageRepository _siteImageRepository;

        public SiteImageManager(ISiteImageRepository siteImageRepository)
        {
            _siteImageRepository = siteImageRepository;
        }

        public SiteImage GetById(int id) => _siteImageRepository.GetById(id);
        public SiteImage GetSingle() => _siteImageRepository.GetSingle();
        public IEnumerable<SiteImage> GetAll() => _siteImageRepository.GetAll();

        public ServiceResult Add(SiteImage entity)
        {
            var existing = _siteImageRepository.GetAll()
                .Any(x => x.ImageUrl.ToLower() == entity.ImageUrl.ToLower());
            if (existing)
                return ServiceResult.Fail("Bu resim zaten mevcut.");

            _siteImageRepository.Add(entity);
            _siteImageRepository.Save();
            return ServiceResult.Ok("Resim başarıyla eklendi.");
        }

        public ServiceResult Update(SiteImage entity)
        {
            var existing = _siteImageRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.ImageUrl = entity.ImageUrl;
            existing.UpdatedDate = DateTime.Now;

            _siteImageRepository.Update(existing);
            _siteImageRepository.Save();
            return ServiceResult.Ok("Resim başarıyla güncellendi.");
        }

        public ServiceResult Delete(SiteImage entity)
        {
            var existing = _siteImageRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _siteImageRepository.Delete(existing);
            _siteImageRepository.Save();
            return ServiceResult.Ok("Resim başarıyla silindi.");
        }
    }
}