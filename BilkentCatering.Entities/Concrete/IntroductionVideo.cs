using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class IntroductionVideo : BaseEntity
    {
        public string Description { get; set; }
        public string VideoUrl { get; set; }
    }
}