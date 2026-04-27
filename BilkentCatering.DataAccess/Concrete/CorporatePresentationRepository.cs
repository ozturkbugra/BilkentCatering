using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class CorporatePresentationRepository : GenericRepository<CorporatePresentation>, ICorporatePresentationRepository
    {
        public CorporatePresentationRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}