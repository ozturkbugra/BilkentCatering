using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class AboutUsRepository : GenericRepository<AboutUs>, IAboutUsRepository
    {
        public AboutUsRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}