using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class Policy : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}