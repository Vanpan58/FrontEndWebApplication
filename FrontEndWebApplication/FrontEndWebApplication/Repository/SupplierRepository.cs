using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository.Interfaces;

namespace FrontEndWebApplication.Repository
{
    public class SupplierRepository : Repository<SupplierDTO>, ISupplierRepository
    {
        public SupplierRepository(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }
    }
}
