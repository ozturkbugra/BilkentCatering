using BilkentCatering.Entities.Abstract;

namespace BilkentCatering.Entities.Concrete
{
    public sealed class CorporatePresentation : BaseEntity
    {
        public string PdfLink { get; set; }
    }
}