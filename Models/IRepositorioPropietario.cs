namespace CamargoInmobiliaria
{
	public interface IRepositorioPropietario : IRepositorio<Propietario>
	{
		Propietario ObtenerPorEmail(string email);

        IList<Propietario> BuscarPorNombre(string nombre);
        
    }
}
