using FrontEndWebApplication.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEndWebApplication.Models.NewModels
{
    public class ProductsVM
    {
        public IEnumerable<SelectListItem> ListProducts { get; set; }
        public ProductDTO Product { get; set; }

    }
}
