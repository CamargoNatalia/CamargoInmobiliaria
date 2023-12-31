using Microsoft.AspNetCore.Mvc;

namespace CamargoInmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {

        RepositorioInmueble repositorio;
         RepositorioPropietario repositorioPropietario;


            public InmueblesController()
            {
                repositorio = new RepositorioInmueble();
                repositorioPropietario = new RepositorioPropietario();
            }
        // GET: Inmuebles
        public ActionResult Index()
        {
            /*var todos = repositorio.ObtenerTodos();
            return View(todos);*/
            ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
            List<Inmueble> listaInmuebles = repositorio.ObtenerTodos();
            ViewBag.CreacionExitosa = TempData["CreacionExitosa"];
            ViewBag.ModificacionExitosa = TempData["ModificacionExitosa"];
            ViewBag.EliminacionExitosa = TempData["EliminacionExitosa"];
            ViewBag.Error = TempData["Error"];
            return View(listaInmuebles);
        }


        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
           
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
                 ViewBag.Propietarios = repositorio.ObtenerTodos();
                return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Create(Inmueble inmueble)
        {
           
            /* try
                    {
                        repositorio.ObtenerPorId(id);
                        var c = repositorio.Alta(inm);
                         if (c > 0)
                        return RedirectToAction(nameof(Index));
                        else{
                    return View();
                        }
                    }
                catch(Exception ex)
                    {
                        throw;
                    }*/
              ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
            try
            {
                // TODO: Add insert logic here
                
                int res = repositorio.Alta(inmueble);
                if(res > 0){
                    TempData["CreacionExitosa"] = 1;
                    return RedirectToAction(nameof(Index));
                }else{
                    return View();
                }

            }
            catch(Exception e){
                TempData["Error"] = 1;
                return RedirectToAction(nameof(Create));
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
            var borrar = repositorio.ObtenerPorId(id);
                return View(borrar);
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
