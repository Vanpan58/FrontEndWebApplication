using FrontEndWebApplication.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEndWebApplication.Models.NewModels
{
    public class CustomersVM
    {
        public IEnumerable<SelectListItem> ListCustomers { get; set; }
        public CustomerDTO Customer { get; set; }

    }
}