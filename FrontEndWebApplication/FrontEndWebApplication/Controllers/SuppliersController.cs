﻿using FrontEndWebApplication.Models.DTO;
using FrontEndWebApplication.Repository;
using FrontEndWebApplication.Repository.Interfaces;
using FrontEndWebApplication.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndWebApplication.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository _supplierRepository)
        {
            this._supplierRepository = _supplierRepository;
        }

        [HttpGet]
        // GET: SupplierController
        public ActionResult Index()
        {
            return View(new SupplierDTO() { });
        }

        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                //Llama al repositorio
                var data = await _supplierRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }


        //GET: SuppliersController/Details/5
        public  ActionResult Details(int id)
        {
            return View();
        }

        // GET: SuppliersController/Create
        //Renderiza la vista
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppliersController/Create
        //Captura los datos y los lleva hacia el endpointpasando por el repositorio --> Nube--> DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SupplierDTO Supplier)
        {
            try
            {
                await _supplierRepository.PostAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers, Supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppliersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            var supplier = new SupplierDTO();

            supplier = await _supplierRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers, id.GetValueOrDefault());
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: SuppliersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SupplierDTO Supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepository.UpdateAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers + Supplier.Id, Supplier);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers, id);

            if (supplier == null)
            {
                return Json(new { success = false, message = "Proveedor no encontrado." });
            }

            var deleteResult = await _supplierRepository.DeleteAsync(UrlResources.UrlBase + UrlResources.UrlSuppliers, id);

            if (deleteResult)
            {
                return Json(new { success = true, message = "Proveedor eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el Proveedor." });
            }
        }


    }
}