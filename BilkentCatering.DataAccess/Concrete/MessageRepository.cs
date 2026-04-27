using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}