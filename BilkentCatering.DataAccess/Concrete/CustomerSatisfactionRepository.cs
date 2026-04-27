using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class CustomerSatisfactionRepository : GenericRepository<CustomerSatisfaction>, ICustomerSatisfactionRepository
    {
        public CustomerSatisfactionRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}