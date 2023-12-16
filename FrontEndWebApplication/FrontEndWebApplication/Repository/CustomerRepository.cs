using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository.Interfaces;

namespace FrontEndWebApplication.Repository
{
    public class CustomerRepository : Repository<CustomerDTO>, ICustomerRepository
    {
        public CustomerRepository(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }
    }
}