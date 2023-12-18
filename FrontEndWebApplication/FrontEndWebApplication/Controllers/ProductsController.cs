using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository.Interfaces;
using FrontEndWebApplication.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _ProductRepository;

        public ProductsController(IProductRepository _ProductRepository)
        {
            this._ProductRepository = _ProductRepository;
        }

        [HttpGet]
        // GET: ProductController
        public ActionResult Index()
        {
            return View(new ProductDTO() { });
        }

        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                //Llama al repositorio
                var data = await _ProductRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlProducts);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }


        //GET: ProductsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var Product = await _ProductRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (Product == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado." });
            }
            return View(Product);
        }

        // GET: ProductsController/Create
        //Renderiza la vista
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        //Captura los datos y los lleva hacia el endpointpasando por el repositorio --> Nube--> DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO Product)
        {
            try
            {
                await _ProductRepository.PostAsync(UrlResources.UrlBase + UrlResources.UrlProducts, Product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var Product = new ProductDTO();

            Product = await _ProductRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id.GetValueOrDefault());
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin, registrado")]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO Product)
        {
            if (ModelState.IsValid)
            {
                await _ProductRepository.UpdateAsync(UrlResources.UrlBase + UrlResources.UrlProducts + Product.Id, Product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Product = await _ProductRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (Product == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado." });
            }

            var deleteResult = await _ProductRepository.DeleteAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (deleteResult)
            {
                return Json(new { success = true, message = "Cliente eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el cliente." });
            }
        }


    }
}