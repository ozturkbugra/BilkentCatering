using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class Contact : BaseEntity
    {
        public string MapLink { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WhatsApp { get; set; }
        public string Instagram { get; set; }
    }
}