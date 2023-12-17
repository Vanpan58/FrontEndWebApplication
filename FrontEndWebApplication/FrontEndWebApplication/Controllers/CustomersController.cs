using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository.Interfaces;
using FrontEndWebApplication.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndWebApplication.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository _customerRepository)
        {
            this._customerRepository = _customerRepository;
        }

        [HttpGet]
        // GET: CustomerController
        public ActionResult Index()
        {
            return View(new CustomerDTO() { });
        }

        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                //Llama al repositorio
                var data = await _customerRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlCustomers);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }


        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
