using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {

        RepositorioInmueble repositorio;

            public InmueblesController()
            {
                repositorio = new RepositorioInmueble();
            }
        // GET: Inmuebles
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            return View(lista);
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Inmueble inm)
        {
             try
                    {
                        repositorio.ObtenerPorId(id);
                        var c = repositorio.Alta(inm);
                         if (c > 0)
                        return RedirectToAction(nameof(Index));
                        else
                    return View();
                    }
                catch(Exception ex)
                    {
                        throw;
                    }
                }


        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
             try
            {
                var x = repositorio.ObtenerPorId(id);
                return View(x);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inm)
        {
            try
            {
                repositorio.ObtenerPorId(id);
                repositorio.Modificacion(inm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            var x = repositorio.ObtenerPorId(id);
                return View(x);
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble inm)
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
}