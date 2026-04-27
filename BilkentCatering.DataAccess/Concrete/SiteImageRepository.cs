using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class SiteImageRepository : GenericRepository<SiteImage>, ISiteImageRepository
    {
        public SiteImageRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}