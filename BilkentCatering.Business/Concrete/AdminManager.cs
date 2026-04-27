using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminManager(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin GetById(int id) => _adminRepository.GetById(id);
        public Admin GetSingle() => _adminRepository.GetSingle();
        public IEnumerable<Admin> GetAll() => _adminRepository.GetAll();

        public ServiceResult Add(Admin entity)
        {
            var existing = _adminRepository.GetAll()
                .Any(x => x.Username.ToLower() == entity.Username.ToLower());
            if (existing)
                return ServiceResult.Fail("Bu kullanıcı adı zaten mevcut.");

            _adminRepository.Add(entity);
            _adminRepository.Save();
            return ServiceResult.Ok("Admin başarıyla eklendi.");
        }

        public ServiceResult Update(Admin entity)
        {
            var existing = _adminRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            var duplicate = _adminRepository.GetAll()
                .Any(x => x.Username.ToLower() == entity.Username.ToLower() && x.Id != entity.Id);
            if (duplicate)
                return ServiceResult.Fail("Bu kullanıcı adı zaten mevcut.");

            existing.Username = entity.Username;
            existing.FullName = entity.FullName;
            existing.Password = entity.Password;
            existing.UpdatedDate = DateTime.Now;

            _adminRepository.Update(existing);
            _adminRepository.Save();
            return ServiceResult.Ok("Admin başarıyla güncellendi.");
        }

        public ServiceResult Delete(Admin entity)
        {
            var existing = _adminRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _adminRepository.Delete(existing);
            _adminRepository.Save();
            return ServiceResult.Ok("Admin başarıyla silindi.");
        }
    }
}