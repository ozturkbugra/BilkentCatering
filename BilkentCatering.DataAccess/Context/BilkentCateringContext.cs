using BilkentCatering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BilkentCatering.DataAccess.Context
{
    public class BilkentCateringContext : DbContext
    {
        public BilkentCateringContext(DbContextOptions<BilkentCateringContext> options) : base(options)
        {
        }

        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<SiteImage> SiteImages { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<CertificateAndDocument> CertificatesAndDocuments { get; set; }
        public DbSet<QualityManagement> QualityManagements { get; set; }
        public DbSet<CustomerSatisfaction> CustomerSatisfactions { get; set; }
        public DbSet<CorporatePresentation> CorporatePresentations { get; set; }
        public DbSet<IntroductionVideo> IntroductionVideos { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}