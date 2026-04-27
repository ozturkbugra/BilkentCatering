using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class CounterRepository : GenericRepository<Counter>, ICounterRepository
    {
        public CounterRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}