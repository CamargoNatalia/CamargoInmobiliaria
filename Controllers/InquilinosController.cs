using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers;

   public class InquilinosController : Controller
    {
            RepositorioInquilino repositorio;
            public InquilinosController()
            {
                repositorio = new RepositorioInquilino();
            }

            // GET: InquilinosController
            public ActionResult Index()
        {
            var lista = repositorio.obtenerTodos();
            return View(lista);
            
        }

        // GET: InquilinosController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var i = repositorio.ObtenerPorId(id);
                return View(i);
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }

        // GET: InquilinosController/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: InquilinosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Inquilino i)
        {
            try
            {
                repositorio.ObtenerPorId(id);
                int res = repositorio.Alta(i);
                if (res > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
                
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: InquilinosController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // POST: InquilinosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino i)
        {
            try
            {
                repositorio.ObtenerPorId(id);
                repositorio.Modificacion(i);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }


        // GET: InquilinosController/Delete/5
        public ActionResult Delete(int id)
        {
           var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
        }

        // POST: InquilinosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino i)
        {
            try
            {
                
                int res = repositorio.Baja(id);
                if (res > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }

