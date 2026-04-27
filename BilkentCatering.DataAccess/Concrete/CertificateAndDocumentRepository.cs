using BilkentCatering.DataAccess.Abstract;
using BilkentCatering.DataAccess.Context;
using BilkentCatering.Entities.Concrete;

namespace BilkentCatering.DataAccess.Concrete
{
    public class CertificateAndDocumentRepository : GenericRepository<CertificateAndDocument>, ICertificateAndDocumentRepository
    {
        public CertificateAndDocumentRepository(BilkentCateringContext context) : base(context)
        {
        }
    }
}