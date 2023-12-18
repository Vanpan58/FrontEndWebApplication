using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository.Interfaces;

namespace FrontEndWebApplication.Repository
{
    public class ProductRepository : Repository<ProductDTO>, IProductRepository
    {
        public ProductRepository(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }
    }
}
