using Microsoft.AspNetCore.Mvc.Rendering;
using FrontEndWebApplication.Models.DTO;

namespace FrontEndWebApplication.Models.NewModels
{
    public class SuppliersVM
    {
        public IEnumerable<SelectListItem> ListSuppliers { get; set; }

        public ProductDTO Supplier { get; set; }
    }
}
