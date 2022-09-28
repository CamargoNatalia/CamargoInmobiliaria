using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria;

		public class RepositorioInmueble 
		//: RepositorioBase
    {
		//public RepositorioInmueble(IConfiguration configuration) : base(configuration){}
		 string connectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public int Alta(Inmueble entidad)
			{
			int res = -1;

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @$"INSERT INTO Inmuebles (Direccion, Uso, Tipo, Ambientes, Latitud, Longitud, Precio, PropietarioId) 
					VALUES(@{nameof(entidad.Direccion)},@{nameof(entidad.Uso)}, @{nameof(entidad.Tipo)}, @{nameof(entidad.Ambientes)},
					@{nameof(entidad.Latitud)}, @{nameof(entidad.Longitud)}, @{nameof(entidad.Precio)}, @{nameof(entidad.Estado)}, @{nameof(entidad.PropietarioId)}
					 SELECT LAST_INSERT_ID();";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					
					command.Parameters.AddWithValue($"@{nameof(entidad.Direccion)}", entidad.Direccion);
					command.Parameters.AddWithValue($"@{nameof(entidad.Uso)}", entidad.Uso);
					command.Parameters.AddWithValue($"@{nameof(entidad.Tipo)}", entidad.Tipo);
					command.Parameters.AddWithValue($"@{nameof(entidad.Ambientes)}", entidad.Ambientes);
					command.Parameters.AddWithValue($"@{nameof(entidad.Latitud)}", entidad.Latitud);
					command.Parameters.AddWithValue($"@{nameof(entidad.Longitud)}", entidad.Longitud);
					command.Parameters.AddWithValue($"@{nameof(entidad.Precio)}", entidad.Precio);
					command.Parameters.AddWithValue($"@{nameof(entidad.PropietarioId)}", entidad.PropietarioId);

					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());					
					connection.Close();
					entidad.IdInmueble = res;

				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Inmuebles WHERE IdInmueble = {id}";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(Inmueble entidad)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
                string sql = $"UPDATE Inmuebles SET " +
					"Direccion=@direccion, Uso=@uso, Tipo=@tipo, Ambientes=@ambientes, Latitud=@latitud,Longitud=@longitud, Precio=@precio, PropietarioId=@propietarioId" +
					"WHERE IdInmueble = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@direccion", entidad.Direccion);
					command.Parameters.AddWithValue("@uso", entidad.Uso);
					command.Parameters.AddWithValue("@tipo", entidad.Tipo);
					command.Parameters.AddWithValue("@ambientes", entidad.Ambientes);
					command.Parameters.AddWithValue("@latitud", entidad.Latitud);
					command.Parameters.AddWithValue("@longitud", entidad.Longitud);
					command.Parameters.AddWithValue("@precio", entidad.Precio);
					command.Parameters.AddWithValue("@propietarioId", entidad.PropietarioId);

					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Inmueble> ObtenerTodos()
		{
			IList<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "SELECT IdInmueble, Direccion, Uso, Tipo, Ambientes, Latitud, Longitud, Precio, PropietarioId," +
					" p.Nombre, p.Apellido" +
                    " FROM Inmuebles i" + 
					" INNER JOIN Propietarios p ON i.PropietarioId = p.Id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();

					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble entidad = new Inmueble
						{
							IdInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
							Uso = reader.GetString(2),
							Tipo = reader.GetString(3),
                            Ambientes = reader.GetInt32(4), 
                            Latitud = reader.GetDecimal(5),
                            Longitud = reader.GetDecimal(6),
							Precio = reader.GetFloat(7),
                            PropietarioId = reader.GetInt32(8),
                            Duenio = new Propietario
                            {
                                Id = reader.GetInt32(9),
                                Nombre = reader.GetString(10),
                                Apellido = reader.GetString(11),
							}
						};
						res.Add(entidad);
					}
					connection.Close();
				}
			}
			return res;
		}
	

		public Inmueble ObtenerPorId(int id)
		{
			Inmueble entidad = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
                string MySql = $"SELECT IdInmueble, Direccion, Ambientes, Superficie, Latitud, Longitud, PropietarioId, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.IdPropietario" +
                    $" WHERE IdInmueble=@id";
				using (MySqlCommand command = new MySqlCommand(MySql, connection))
				{
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						entidad = new Inmueble
						{
                            IdInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Latitud = reader.GetDecimal(3),
                            Longitud = reader.GetDecimal(4),
							Precio = reader.GetFloat(5),
                            PropietarioId = reader.GetInt32(6),
                            Duenio = new Propietario
                            {
                                Id = reader.GetInt32(7),
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            }
                        };
					}
					connection.Close();
				}
			}
			return entidad;
        }

        public IList<Inmueble> BuscarPorPropietario(int Id)
        {
            List<Inmueble> res = new List<Inmueble>();
            Inmueble entidad = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string MySql = $"SELECT Id, Direccion, Ambientes, Latitud, Longitud,Precio, PropietarioId, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.IdPropietario" +
                    $" WHERE PropietarioId=@Id";
                using (MySqlCommand command = new MySqlCommand(MySql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = Id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Latitud = reader.GetDecimal(4),
                            Longitud = reader.GetDecimal(5),
							Precio = reader.GetFloat(6),
                            PropietarioId = reader.GetInt32(7),
                            Duenio = new Propietario
                            {
                                Id = reader.GetInt32(6),
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            }
                        };
                        res.Add(entidad);
                    }
                    connection.Close();
                }
            }
            return res;
        }
		public IList<Inmueble> ObtenerDisponibles()
        {
            IList<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "SELECT i.IdInmueble, p.Id, Direccion, Ambientes, Latitud, Longitud , Tipo, Uso, Precio, Estado," +
					" p.Nombre, p.Apellido" +
                    " FROM Inmuebles i INNER JOIN Propietarios p ON i.IdInmueble = p.Id"+
					$" WHERE estado=1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble inmueble = new Inmueble
						{
							IdInmueble = reader.GetInt32(0),
                           PropietarioId = reader.GetInt32(1),
                            Direccion = reader.GetString(2),
                            Ambientes = reader.GetInt32(3),
							Latitud = reader.GetDecimal(4),
							Longitud = reader.GetDecimal(5),
							Tipo = reader.GetString(6),
							Uso = reader.GetString(7),
                            Precio = reader.GetFloat(8),
                            Estado = reader.GetInt32(9),

							Duenio = new Propietario(){
								Id = reader.GetInt32(1),
                                Nombre = reader.GetString(10),
                                Apellido = reader.GetString(11),	
							}
						};
						res.Add(inmueble);
						};
						
					}
					connection.Close();
				}
			
			return res;
		}

		
    }

