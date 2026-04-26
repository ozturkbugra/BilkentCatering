using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class Counter : BaseEntity
    {
        public int CityCount { get; set; }
        public int DailyProductionCount { get; set; }
        public int SafetyRate { get; set; }
    }
}