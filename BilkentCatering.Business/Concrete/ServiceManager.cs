using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class ServicesManager : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesManager(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Service GetById(int id) => _serviceRepository.GetById(id);
        public Service GetSingle() => _serviceRepository.GetSingle();
        public IEnumerable<Service> GetAll() => _serviceRepository.GetAll();

        public ServiceResult Add(Service entity)
        {
            var existing = _serviceRepository.GetAll()
                .Any(x => x.Title.ToLower() == entity.Title.ToLower());
            if (existing)
                return ServiceResult.Fail("Bu başlıkta bir hizmet zaten mevcut.");

            _serviceRepository.Add(entity);
            _serviceRepository.Save();
            return ServiceResult.Ok("Hizmet başarıyla eklendi.");
        }

        public ServiceResult Update(Service entity)
        {
            var existing = _serviceRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            var duplicate = _serviceRepository.GetAll()
                .Any(x => x.Title.ToLower() == entity.Title.ToLower() && x.Id != entity.Id);
            if (duplicate)
                return ServiceResult.Fail("Bu başlıkta bir hizmet zaten mevcut.");

            existing.Title = entity.Title;
            existing.Description = entity.Description;
            existing.ImageUrl = entity.ImageUrl;
            existing.UpdatedDate = DateTime.Now;

            _serviceRepository.Update(existing);
            _serviceRepository.Save();
            return ServiceResult.Ok("Hizmet başarıyla güncellendi.");
        }

        public ServiceResult Delete(Service entity)
        {
            var existing = _serviceRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _serviceRepository.Delete(existing);
            _serviceRepository.Save();
            return ServiceResult.Ok("Hizmet başarıyla silindi.");
        }
    }
}