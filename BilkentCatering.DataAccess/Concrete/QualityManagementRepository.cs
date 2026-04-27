using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class QualityManagementRepository : GenericRepository<QualityManagement>, IQualityManagementRepository
    {
        public QualityManagementRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}