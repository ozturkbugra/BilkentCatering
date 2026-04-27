using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class SiteSettingsRepository : GenericRepository<SiteSettings>, ISiteSettingsRepository
    {
        public SiteSettingsRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}