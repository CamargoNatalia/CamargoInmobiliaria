using System.Data;
using MySql.Data.MySqlClient;


namespace CamargoInmobiliaria{

public class RepositorioInmueble
    {
        string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

        public RepositorioInmueble()
        {

        }

public int Alta(Inmueble entidad)
		{
			int res = -1;
			using (MySqlConnection conn = new MySqlConnection(ConnectionString))
			{
				string sql = $"INSERT INTO Inmuebles (Uso, Tipo, Direccion, Ambientes, Latitud, Longitud, precio, PropietarioId) " +
					"VALUES (@uso, @tipo, @direccion, @ambientes, @latitud, @longitud, @precio, @propietarioId);" +
					"SELECT LAST_INSERT_INTO();";
				using (var command = new MySqlCommand(sql, conn))
				{
					command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@uso", entidad.Uso);
                    command.Parameters.AddWithValue("@tipo", entidad.Tipo);
					command.Parameters.AddWithValue("@direccion", entidad.Direccion);
					command.Parameters.AddWithValue("@ambientes", entidad.Ambientes);
					command.Parameters.AddWithValue("@latitud", entidad.Latitud);
					command.Parameters.AddWithValue("@longitud", entidad.Longitud);
                    command.Parameters.AddWithValue("@precio", entidad.Precio);
					command.Parameters.AddWithValue("@propietarioId", entidad.PropietarioId);
					conn.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					entidad.inmueblesId = res;
					conn.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				string sql = $"DELETE FROM Inmuebles WHERE inmueblesId = {id}";
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
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
                string sql = "UPDATE Inmuebles SET " +
					"Uso=@uso, Tipo=@tipo, Direccion=@direccion, Ambientes=@ambientes,Latitud=@latitud, Longitud=@longitud, Precio=@precio, PropietarioId=@propietarioId " +
					"WHERE inmueblesId = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.AddWithValue("@uso", entidad.Uso);
                    command.Parameters.AddWithValue("@tipo", entidad.Tipo);
					command.Parameters.AddWithValue("@direccion", entidad.Direccion);
					command.Parameters.AddWithValue("@ambientes", entidad.Ambientes);
					command.Parameters.AddWithValue("@latitud", entidad.Latitud);
					command.Parameters.AddWithValue("@longitud", entidad.Longitud);
                    command.Parameters.AddWithValue("@precio", entidad.Precio);
					command.Parameters.AddWithValue("@propietarioId", entidad.PropietarioId);
					command.Parameters.AddWithValue("@id", entidad.inmueblesId);
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

			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				string sql = "SELECT inmueblesId, Uso, Tipo, Direccion, Ambientes, Latitud, Longitud, Precio, PropietarioId," +
					" p.Nombre, p.Apellido" +
                    " FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						var entidad = new Inmueble
						{
							inmueblesId = reader.GetInt32(0),
                            Uso = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            Direccion = reader.GetString(3),
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
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
                string sql = $"SELECT inmueblesId, Uso, Tipo, Direccion, Ambientes, Latitud, Longitud, Precio, PropietarioId, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.IdPropietario" +
                    $" WHERE inmueblesId=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						entidad = new Inmueble
						{
                            inmueblesId = reader.GetInt32(0),
                            Uso = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            Direccion = reader.GetString(3),
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
					}
					connection.Close();
				}
			}
			return entidad;
        }

        public IList<Inmueble> BuscarPorPropietario(int id)
        {
            List<Inmueble> res = new List<Inmueble>();
            Inmueble entidad = null;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT inmueblesId, Uso, Tipo, Direccion, Ambientes, Latitud, Longitud, Precio, PropietarioId, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id" +
                    $" WHERE PropietarioId=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new Inmueble
                        {
                            inmueblesId = reader.GetInt32(0),
                            Uso = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            Direccion = reader.GetString(3),
                            Ambientes = reader.GetInt32(4),
                            Latitud = reader.GetDecimal(5),
                            Longitud = reader.GetDecimal(6),
                            Precio = reader.GetFloat(7),
                            PropietarioId = reader.GetInt32(8),
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
    }
}
