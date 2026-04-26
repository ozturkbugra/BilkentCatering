using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class Admin : BaseEntity
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}