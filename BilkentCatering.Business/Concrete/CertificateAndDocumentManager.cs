using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class CertificateAndDocumentManager : ICertificateAndDocumentService
    {
        private readonly ICertificateAndDocumentRepository _certificateAndDocumentRepository;

        public CertificateAndDocumentManager(ICertificateAndDocumentRepository certificateAndDocumentRepository)
        {
            _certificateAndDocumentRepository = certificateAndDocumentRepository;
        }

        public CertificateAndDocument GetById(int id) => _certificateAndDocumentRepository.GetById(id);
        public CertificateAndDocument GetSingle() => _certificateAndDocumentRepository.GetSingle();
        public IEnumerable<CertificateAndDocument> GetAll() => _certificateAndDocumentRepository.GetAll();

        public ServiceResult Add(CertificateAndDocument entity)
        {
            var existing = _certificateAndDocumentRepository.GetAll()
                .Any(x => x.Title.ToLower() == entity.Title.ToLower());
            if (existing)
                return ServiceResult.Fail("Bu başlıkta bir belge/sertifika zaten mevcut.");

            _certificateAndDocumentRepository.Add(entity);
            _certificateAndDocumentRepository.Save();
            return ServiceResult.Ok("Belge/Sertifika başarıyla eklendi.");
        }

        public ServiceResult Update(CertificateAndDocument entity)
        {
            var existing = _certificateAndDocumentRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            var duplicate = _certificateAndDocumentRepository.GetAll()
                .Any(x => x.Title.ToLower() == entity.Title.ToLower() && x.Id != entity.Id);
            if (duplicate)
                return ServiceResult.Fail("Bu başlıkta bir belge/sertifika zaten mevcut.");

            existing.Title = entity.Title;
            existing.Description = entity.Description;
            existing.PdfLink = entity.PdfLink;
            existing.UpdatedDate = DateTime.Now;

            _certificateAndDocumentRepository.Update(existing);
            _certificateAndDocumentRepository.Save();
            return ServiceResult.Ok("Belge/Sertifika başarıyla güncellendi.");
        }

        public ServiceResult Delete(CertificateAndDocument entity)
        {
            var existing = _certificateAndDocumentRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _certificateAndDocumentRepository.Delete(existing);
            _certificateAndDocumentRepository.Save();
            return ServiceResult.Ok("Belge/Sertifika başarıyla silindi.");
        }
    }
}