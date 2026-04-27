using BilkentCatering.Business.Abstract;
using BilkentCatering.Business.Concrete;
using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Concrete;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace BilkentCatering.UI.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<BilkentCateringContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // FileUploadService
            services.AddScoped<FileUploadService>();

            // DataAccess
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICorporatePresentationRepository, CorporatePresentationRepository>();
            services.AddScoped<ICounterRepository, CounterRepository>();
            services.AddScoped<ICustomerSatisfactionRepository, CustomerSatisfactionRepository>();
            services.AddScoped<IIntroductionVideoRepository, IntroductionVideoRepository>();
            services.AddScoped<IPolicyRepository, PolicyRepository>();
            services.AddScoped<IQualityManagementRepository, QualityManagementRepository>();
            services.AddScoped<ISiteSettingsRepository, SiteSettingsRepository>();
            services.AddScoped<ICertificateAndDocumentRepository, CertificateAndDocumentRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISiteImageRepository, SiteImageRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            // Business
            services.AddScoped<IAboutUsService, AboutUsManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<ICorporatePresentationService, CorporatePresentationManager>();
            services.AddScoped<ICounterService, CounterManager>();
            services.AddScoped<ICustomerSatisfactionService, CustomerSatisfactionManager>();
            services.AddScoped<IIntroductionVideoService, IntroductionVideoManager>();
            services.AddScoped<IPolicyService, PolicyManager>();
            services.AddScoped<IQualityManagementService, QualityManagementManager>();
            services.AddScoped<ISiteSettingsService, SiteSettingsManager>();
            services.AddScoped<ICertificateAndDocumentService, CertificateAndDocumentManager>();
            services.AddScoped<IJobApplicationService, JobApplicationManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IServicesService, ServicesManager>();
            services.AddScoped<ISiteImageService, SiteImageManager>();
            services.AddScoped<IAdminService, AdminManager>();
        }
    }
}