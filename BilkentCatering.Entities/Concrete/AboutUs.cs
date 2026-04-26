using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class AboutUs : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ExperienceYear { get; set; }
    }
}