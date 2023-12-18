using Microsoft.AspNetCore.Mvc.Rendering;
using FrontEndWebApplication.Models.DTO;

namespace FrontEndWebApplication.Models.NewModels
{
    public class SupplierMV
    {
        public IEnumerable<SelectListItem> ListSuppliers { get; set; }

        public ProductDTO Proveedor { get; set; }
    }
}
