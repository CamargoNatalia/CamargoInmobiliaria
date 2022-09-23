using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers
{

 public class ContratosController : Controller
    {
      private readonly IConfiguration config;
    private readonly IRepositorioContrato repo;
    private readonly IRepositorioInmueble repoInmueble;

          RepositorioInquilino repoInquilino = new RepositorioInquilino();
    
         
     public ContratosController(RepositorioContrato repo, RepositorioInmueble repoInmueble, RepositorioInquilino repoInquilino, IConfiguration config)
        {

            this.repoInquilino = repoInquilino;
        }
      


        // GET: Contrato
        public ActionResult Index()
        {
            var aux = repo.ObtenerTodos();
            return View(aux);
        }
       
        // GET: Contrato/Create
        public ActionResult Create()
        {

            var listInquilino = repoInquilino.obtenerTodos();
           var listInmueble = repoInmueble.ObtenerDisponibles();

            ViewBag.inmueble = listInmueble;
            ViewBag.inquilino = listInquilino;
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato c)
        {
            if(!ModelState.IsValid){
                return View();
            }else{
                if(c.fechaInicio > c.fechaFin){
                TempData["Mensaje"] = "La Fecha de Fin debe ser superior a la Fecha de Inicio";
                
                return View();
                }else{
                    try
                    {
                        var alta = repo.Alta(c);
                
                        return RedirectToAction(nameof(Index));
                    }
                catch(Exception ex)
                    {
                        throw;
                    }
                }
            
            }
            
            
        }

        public ActionResult Details(int id)
        {
            var contrato = repo.ObtenerPorId(id);
            return View(contrato);
        }
        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            var contrato = repo.ObtenerPorId(id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            var c = repo.ObtenerPorId(id);
            return View(c);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repo.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}