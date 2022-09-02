using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers;

    public class PropietariosController : Controller
    {

        RepositorioPropietario repositorio;
            public PropietariosController()
            {
                repositorio = new RepositorioPropietario();
            }
        // GET: Propietarios
        public ActionResult Index()
        {
            var lista = repositorio.obtenerTodos();
            return View(lista);
            
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var p = repositorio.ObtenerPorId(id);
                return View(p);
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
             
                return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        { 
            try
            {
            
                int res = repositorio.Alta(p);
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

        // GET: Propietarios/Edit/5
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

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario p)
        {
           try
            {
                repositorio.ObtenerPorId(id);
                
                repositorio.Modificacion(p);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario p)
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

