using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class QualityManagementManager : IQualityManagementService
    {
        private readonly IQualityManagementRepository _qualityManagementRepository;

        public QualityManagementManager(IQualityManagementRepository qualityManagementRepository)
        {
            _qualityManagementRepository = qualityManagementRepository;
        }

        public QualityManagement GetById(int id) => _qualityManagementRepository.GetById(id);
        public QualityManagement GetSingle() => _qualityManagementRepository.GetSingle();
        public IEnumerable<QualityManagement> GetAll() => _qualityManagementRepository.GetAll();

        public ServiceResult Add(QualityManagement entity)
        {
            var existing = _qualityManagementRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Kalite yönetimi kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _qualityManagementRepository.Add(entity);
            _qualityManagementRepository.Save();
            return ServiceResult.Ok("Kalite yönetimi bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(QualityManagement entity)
        {
            var existing = _qualityManagementRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Title = entity.Title;
            existing.ShortDescription = entity.ShortDescription;
            existing.LongDescription = entity.LongDescription;
            existing.ImageUrl = entity.ImageUrl;
            existing.UpdatedDate = DateTime.Now;

            _qualityManagementRepository.Update(existing);
            _qualityManagementRepository.Save();
            return ServiceResult.Ok("Kalite yönetimi bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(QualityManagement entity)
        {
            var existing = _qualityManagementRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _qualityManagementRepository.Delete(existing);
            _qualityManagementRepository.Save();
            return ServiceResult.Ok("Kalite yönetimi bilgisi başarıyla silindi.");
        }
    }
}