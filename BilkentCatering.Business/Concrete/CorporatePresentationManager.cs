using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class CorporatePresentationManager : ICorporatePresentationService
    {
        private readonly ICorporatePresentationRepository _corporatePresentationRepository;

        public CorporatePresentationManager(ICorporatePresentationRepository corporatePresentationRepository)
        {
            _corporatePresentationRepository = corporatePresentationRepository;
        }

        public CorporatePresentation GetById(int id) => _corporatePresentationRepository.GetById(id);
        public CorporatePresentation GetSingle() => _corporatePresentationRepository.GetSingle();
        public IEnumerable<CorporatePresentation> GetAll() => _corporatePresentationRepository.GetAll();

        public ServiceResult Add(CorporatePresentation entity)
        {
            var existing = _corporatePresentationRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Kurumsal sunum kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _corporatePresentationRepository.Add(entity);
            _corporatePresentationRepository.Save();
            return ServiceResult.Ok("Kurumsal sunum başarıyla eklendi.");
        }

        public ServiceResult Update(CorporatePresentation entity)
        {
            var existing = _corporatePresentationRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.PdfLink = entity.PdfLink;
            existing.UpdatedDate = DateTime.Now;

            _corporatePresentationRepository.Update(existing);
            _corporatePresentationRepository.Save();
            return ServiceResult.Ok("Kurumsal sunum başarıyla güncellendi.");
        }

        public ServiceResult Delete(CorporatePresentation entity)
        {
            var existing = _corporatePresentationRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _corporatePresentationRepository.Delete(existing);
            _corporatePresentationRepository.Save();
            return ServiceResult.Ok("Kurumsal sunum başarıyla silindi.");
        }
    }
}