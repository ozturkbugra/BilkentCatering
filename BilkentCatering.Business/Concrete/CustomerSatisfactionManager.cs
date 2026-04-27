using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class CustomerSatisfactionManager : ICustomerSatisfactionService
    {
        private readonly ICustomerSatisfactionRepository _customerSatisfactionRepository;

        public CustomerSatisfactionManager(ICustomerSatisfactionRepository customerSatisfactionRepository)
        {
            _customerSatisfactionRepository = customerSatisfactionRepository;
        }

        public CustomerSatisfaction GetById(int id) => _customerSatisfactionRepository.GetById(id);
        public CustomerSatisfaction GetSingle() => _customerSatisfactionRepository.GetSingle();
        public IEnumerable<CustomerSatisfaction> GetAll() => _customerSatisfactionRepository.GetAll();

        public ServiceResult Add(CustomerSatisfaction entity)
        {
            var existing = _customerSatisfactionRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Müşteri memnuniyeti kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _customerSatisfactionRepository.Add(entity);
            _customerSatisfactionRepository.Save();
            return ServiceResult.Ok("Müşteri memnuniyeti bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(CustomerSatisfaction entity)
        {
            var existing = _customerSatisfactionRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Title = entity.Title;
            existing.ShortDescription = entity.ShortDescription;
            existing.LongDescription = entity.LongDescription;
            existing.ImageUrl = entity.ImageUrl;
            existing.UpdatedDate = DateTime.Now;

            _customerSatisfactionRepository.Update(existing);
            _customerSatisfactionRepository.Save();
            return ServiceResult.Ok("Müşteri memnuniyeti bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(CustomerSatisfaction entity)
        {
            var existing = _customerSatisfactionRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _customerSatisfactionRepository.Delete(existing);
            _customerSatisfactionRepository.Save();
            return ServiceResult.Ok("Müşteri memnuniyeti bilgisi başarıyla silindi.");
        }
    }
}