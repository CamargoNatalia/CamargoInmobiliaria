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
        RepositorioContrato repo;

            public ContratosController()
            {
                repo = new RepositorioContrato();
            }


        // GET: Contratos
        public ActionResult Index()
        {
            
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        // GET: Contratos/Details/5
        public ActionResult Details(int id)
        {

           var lista = repo.ObtenerPorId(id);
           return View(lista);
        }

        // GET: Contratos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Contrato c)
        {
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

        // GET: Contratos/Edit/5
        public ActionResult Edit(int id)
        {
            var entidad = repo.ObtenerPorId(id);
            return View(entidad);
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato c)
        {
            try
            {
                repo.ObtenerPorId(id);
                repo.Modificacion(c);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contratos/Delete/5
        public ActionResult Delete(int id)
        {
            var entidad = repo.ObtenerPorId(id);
            return View(entidad);
        }

        // POST: Contratos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato c)
        {
            try
            {
               int res = repo.Baja(id);
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
}