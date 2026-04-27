using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}