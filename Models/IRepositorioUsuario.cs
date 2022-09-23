namespace CamargoInmobiliaria
{
	public interface IRepositorioUsuario : IRepositorio<Usuario>
	{
		Usuario ObtenerPorEmail(string email);
    }
}