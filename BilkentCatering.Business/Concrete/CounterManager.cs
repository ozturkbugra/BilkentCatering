using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class CounterManager : ICounterService
    {
        private readonly ICounterRepository _counterRepository;

        public CounterManager(ICounterRepository counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public Counter GetById(int id) => _counterRepository.GetById(id);
        public Counter GetSingle() => _counterRepository.GetSingle();
        public IEnumerable<Counter> GetAll() => _counterRepository.GetAll();

        public ServiceResult Add(Counter entity)
        {
            var existing = _counterRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Sayaç kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _counterRepository.Add(entity);
            _counterRepository.Save();
            return ServiceResult.Ok("Sayaç bilgisi başarıyla eklendi.");
        }

        public ServiceResult Update(Counter entity)
        {
            var existing = _counterRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.CityCount = entity.CityCount;
            existing.DailyProductionCount = entity.DailyProductionCount;
            existing.SafetyRate = entity.SafetyRate;
            existing.UpdatedDate = DateTime.Now;

            _counterRepository.Update(existing);
            _counterRepository.Save();
            return ServiceResult.Ok("Sayaç bilgisi başarıyla güncellendi.");
        }

        public ServiceResult Delete(Counter entity)
        {
            var existing = _counterRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _counterRepository.Delete(existing);
            _counterRepository.Save();
            return ServiceResult.Ok("Sayaç bilgisi başarıyla silindi.");
        }
    }
}