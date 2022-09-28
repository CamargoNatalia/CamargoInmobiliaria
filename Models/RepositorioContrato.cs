using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria;

public class RepositorioContrato 
//: RepositorioBase
{

/*public RepositorioContrato(IConfiguration configuration) : base(configuration)
		
	 {
	 }
*/
		 string connectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

   
    public int Alta(Contrato contrato){

        int res = -1;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Contratos (fechaInicio, fechaFin, montoMensual, InmuebleId, InquilinoId) " +
					"VALUES (@fechaInicio, @fechaFin, @montoMensual, @inmuebleId, @inquilinoId);" +
					"SELECT LAST_INSERT_INTO();";
                    using (var command = new MySqlCommand(sql, conn))
				{
					command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@fechaIncio", contrato.fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", contrato.fechaFin);
					command.Parameters.AddWithValue("@montoMensual", contrato.montoMensual);
					command.Parameters.AddWithValue("@inmuebleId", contrato.InmuebleId);
					command.Parameters.AddWithValue("@inquilinoId", contrato.InquilinoId);
					conn.Open();
					res = Convert.ToInt32(command.ExecuteScalar());

					conn.Close();
					contrato.Id = res;
				}
			}
			return res;
			}	

    public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
                string sql = $"DELETE FROM Contratos WHERE Id = {id}";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
		}
		
		public int Modificacion(Contrato contrato)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
                string sql = "UPDATE Contratos SET " +
					"fechaInicio=@fechaInicio, fechaFin=@fechaFin, montoMensual=@montoMensual, InmuebleId=@inmuebleId, InquilinoId=@inquilinoId " +
					"WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.AddWithValue("@fechaInicio", contrato.fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", contrato.fechaFin);
					command.Parameters.AddWithValue("@montoMensual", contrato.montoMensual);
					command.Parameters.AddWithValue("@inmuebleId", contrato.InmuebleId);
					command.Parameters.AddWithValue("@inquilinoId", contrato.InquilinoId);
					command.Parameters.AddWithValue("@id", contrato.Id);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		 public IList<Contrato> obtenerTodos()
        {
            var res = new List<Contrato>();
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT c.id,fechaInicio, fechaFin, InquilinoId, InmuebleId, inq.Nombre, inq.Apellido, inm.Direccion, inm.precio FROM Contratos c INNER JOIN Inquilinos inq ON c.InquilinoId = inq.Id INNER JOIN Inmuebles inm ON c.InmuebleId = inm.IdInmueble";
   
             using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
					comm.CommandType = CommandType.Text;
                    conn.Open();


                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
						
                        var c = new Contrato
                        {
                            Id = reader.GetInt32(0),
							fechaInicio = reader.GetDateTime(1),
                            fechaFin = reader.GetDateTime(2),
                            montoMensual = reader.GetFloat(3),
                            InquilinoId = reader.GetInt32(4),
							InmuebleId = reader.GetInt32(5),
						 
                            inq = new Inquilino
                            {
                                Id = reader.GetInt32(4),
                                Nombre = reader.GetString(6),
                                Apellido = reader.GetString(7),							
							
							},
							inm = new Inmueble(){
								IdInmueble = reader.GetInt32(5),
								Direccion = reader.GetString(8),
                                
							}
							
						};
						res.Add(c);
					}
					conn.Close();
				}
			}
			return res;
            
        }

		public Contrato ObtenerPorId(int id){
			Contrato c = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{

				string sql = "SELECT c.id,fechaInicio, fechaFin, InquilinoId, InmuebleId, inq.Nombre, inq.Apellido, inm.Direccion, inm.precio FROM Contratos c INNER JOIN Inquilinos inq ON c.InquilinoId = inq.Id INNER JOIN Inmuebles inm ON c.InmuebleId = inm.IdInmueble;";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						 c = new Contrato
						{
							Id = reader.GetInt32(0),
                            fechaInicio = reader.GetDateTime(1),
                            fechaFin = reader.GetDateTime(2),
							montoMensual = reader.GetFloat(3),
							InquilinoId = reader.GetInt32(4),
                            InmuebleId = reader.GetInt32(5),
                            inq = new Inquilino(){
								Id = reader.GetInt32(4),
                                Nombre = reader.GetString(6),
                                Apellido = reader.GetString(7),	
							},
							inm = new Inmueble(){
								IdInmueble = reader.GetInt32(5),
								Direccion = reader.GetString(8),
                                Precio = reader.GetFloat(9),
							}
                            
						};
						
					}
					connection.Close();
				}
			}
			return c;
		}
public IList<Contrato> ObtenerPorInmueble(int id)
        {
            List<Contrato> res = new List<Contrato>();
            
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "SELECT c.id, InquilinoId, InmuebleId, fechaInicio, fechaFin, inq.Nombre, inq.Apellido, inm.Direccion FROM Contratos c INNER JOIN Inquilinos inq ON c.InquilinoId = inq.Id INNER JOIN Inmuebles inm ON c.InmuebleId = inq.Id WHERE c.InmuebleId = @id;";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato c = new Contrato
						{
							Id = reader.GetInt32(0),
                            fechaInicio = reader.GetDateTime(1),
                            fechaFin = reader.GetDateTime(2),
							montoMensual = reader.GetFloat(3),
							InquilinoId = reader.GetInt32(4),
                            InmuebleId = reader.GetInt32(5),
                            inq = new Inquilino(){
								Id = reader.GetInt32(4),
                                Nombre = reader.GetString(6),
                                Apellido = reader.GetString(7),	
							},
							inm = new Inmueble(){
								IdInmueble = reader.GetInt32(5),
								Direccion = reader.GetString(8),
                                
							}
                            
						};
						res.Add(c);
					}
					connection.Close();
				}
			}
			return res;
		}
}

		
    
