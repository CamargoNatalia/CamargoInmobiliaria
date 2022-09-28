using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers
{

 public class PagosController : Controller
    {
        RepositorioPago repoPago;
        RepositorioContrato repoContrato;
            public PagosController()
            {
                repoPago = new RepositorioPago();
                repoContrato = new RepositorioContrato();
            }

 public ActionResult Index()
        {
            var list = repoPago.ObtenerTodos();
            return View(list);
        }
        // GET: Pago/Create
        public ActionResult Create(int id)
        {   
            var contrato = repoContrato.ObtenerPorId(id);
            ViewBag.ContratoId = id;
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Pago p)
        {
            
            if(!ModelState.IsValid){
                return View();
            }else{
                try
                    {
                        var alta = repoPago.Alta(p);
                        return RedirectToAction(nameof(Index));
                    }
                catch(Exception ex)
                    {
                        throw;
                    }
                }
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
             try
            {
                var entidad = repoPago.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago p)
        {
            try
            {
                repoPago.ObtenerPorId(id);
                repoPago.Modificacion(p);
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

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            var pagos = repoPago.ObtenerPorId(id);
                return View(pagos);
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago p)
        {
            try
            {
                
                int res = repoPago.Baja(id);
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

