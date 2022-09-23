
namespace CamargoInmobiliaria{

    public interface IRepositorioInmueble : IRepositorio<Inmueble>
	{
		IList<Inmueble> BuscarPorPropietario(int Id);
        IList<Inmueble> ObtenerDisponibles() ;
    }
}

