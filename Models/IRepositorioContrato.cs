
namespace CamargoInmobiliaria{

    public interface IRepositorioContrato : IRepositorio<Contrato>
	{
		IList<Contrato> ObtenerPorId(int Id);
        IList<Contrato> ObtenerPorInmueble(int id) ;
    }
}