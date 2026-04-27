using BilkentCatering.Business.Abstract;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.Business.Concrete
{
    public class IntroductionVideoManager : IIntroductionVideoService
    {
        private readonly IIntroductionVideoRepository _introductionVideoRepository;

        public IntroductionVideoManager(IIntroductionVideoRepository introductionVideoRepository)
        {
            _introductionVideoRepository = introductionVideoRepository;
        }

        public IntroductionVideo GetById(int id) => _introductionVideoRepository.GetById(id);
        public IntroductionVideo GetSingle() => _introductionVideoRepository.GetSingle();
        public IEnumerable<IntroductionVideo> GetAll() => _introductionVideoRepository.GetAll();

        public ServiceResult Add(IntroductionVideo entity)
        {
            var existing = _introductionVideoRepository.GetSingle();
            if (existing != null)
                return ServiceResult.Fail("Tanıtım filmi kaydı zaten mevcut. Yeni kayıt eklenemez.");

            _introductionVideoRepository.Add(entity);
            _introductionVideoRepository.Save();
            return ServiceResult.Ok("Tanıtım filmi başarıyla eklendi.");
        }

        public ServiceResult Update(IntroductionVideo entity)
        {
            var existing = _introductionVideoRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Güncellenecek kayıt bulunamadı.");

            existing.Description = entity.Description;
            existing.VideoUrl = entity.VideoUrl;
            existing.UpdatedDate = DateTime.Now;

            _introductionVideoRepository.Update(existing);
            _introductionVideoRepository.Save();
            return ServiceResult.Ok("Tanıtım filmi başarıyla güncellendi.");
        }

        public ServiceResult Delete(IntroductionVideo entity)
        {
            var existing = _introductionVideoRepository.GetById(entity.Id);
            if (existing == null)
                return ServiceResult.Fail("Silinecek kayıt bulunamadı.");

            _introductionVideoRepository.Delete(existing);
            _introductionVideoRepository.Save();
            return ServiceResult.Ok("Tanıtım filmi başarıyla silindi.");
        }
    }
}