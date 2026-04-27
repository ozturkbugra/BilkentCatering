using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class PolicyManager : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyManager(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public Policy GetById(int id) => _policyRepository.GetById(id);
        public Policy GetSingle() => _policyRepository.GetSingle();
        public IEnumerable<Policy> GetAll() => _policyRepository.GetAll();

        public ServiceResult Add(Policy entity)
        {
            var existing = _policyRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Politika kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _policyRepository.Add(entity);
            _policyRepository.Save();
            return ServiceResult.Ok("Politika bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(Policy entity)
        {
            var existing = _policyRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Title = entity.Title;
            existing.ShortDescription = entity.ShortDescription;
            existing.LongDescription = entity.LongDescription;
            existing.ImageUrl = entity.ImageUrl;
            existing.UpdatedDate = DateTime.Now;

            _policyRepository.Update(existing);
            _policyRepository.Save();
            return ServiceResult.Ok("Politika bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(Policy entity)
        {
            var existing = _policyRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _policyRepository.Delete(existing);
            _policyRepository.Save();
            return ServiceResult.Ok("Politika bilgisi başarıyla silindi.");
        }
    }
}