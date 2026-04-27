using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class JobApplicationManager : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationManager(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public JobApplication GetById(int id) => _jobApplicationRepository.GetById(id);
        public JobApplication GetSingle() => _jobApplicationRepository.GetSingle();
        public IEnumerable<JobApplication> GetAll() => _jobApplicationRepository.GetAll();

        public ServiceResult Add(JobApplication entity)
        {
            var existing = _jobApplicationRepository.GetAll()
                .Any(x => x.Email.ToLower() == entity.Email.ToLower() && x.Position == entity.Position);
            if (existing)
                return ServiceResult.Fail("Bu e-posta ile aynı pozisyona daha önce başvuru yapılmış.");

            entity.ApplicationDate = DateTime.Now;
            _jobApplicationRepository.Add(entity);
            _jobApplicationRepository.Save();
            return ServiceResult.Ok("Başvurunuz başarıyla alındı.");
        }

        public ServiceResult Update(JobApplication entity)
        {
            var existing = _jobApplicationRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.IsRead = entity.IsRead;
            existing.UpdatedDate = DateTime.Now;

            _jobApplicationRepository.Update(existing);
            _jobApplicationRepository.Save();
            return ServiceResult.Ok("Başvuru güncellendi.");
        }

        public ServiceResult Delete(JobApplication entity)
        {
            var existing = _jobApplicationRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _jobApplicationRepository.Delete(existing);
            _jobApplicationRepository.Save();
            return ServiceResult.Ok("Başvuru başarıyla silindi.");
        }
    }
}