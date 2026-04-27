using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class IntroductionVideoRepository : GenericRepository<IntroductionVideo>, IIntroductionVideoRepository
    {
        public IntroductionVideoRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}