using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class JobApplication : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string ExperienceYear { get; set; }
        public string? CvFilePath { get; set; }
        public string? Description { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}