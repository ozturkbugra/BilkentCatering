using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class SiteSettings : BaseEntity
    {
        public string Slogan { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CompanyName { get; set; }
        public string Icon { get; set; }
        public string LargeIcon { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public bool IsLogoVisible { get; set; }
        public bool IsCompanyNameVisible { get; set; }
        public string? PageTitleImage { get; set; }

    }
}
