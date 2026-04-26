using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class CertificateAndDocument : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfLink { get; set; }
    }
}