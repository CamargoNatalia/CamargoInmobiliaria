using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria
{

    public class RepositorioInmueble
    {

        string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        
        public int Alta(Inmueble inm)
        {
            var res = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"INSERT INTO Inmuebles (Direccion,Ambientes,Superficie,Latitud,Longitud,Precio,Tipo,Uso)
                               VALUES (@{nameof(inm.Direccion)},@{nameof(inm.Ambientes)}, @{nameof(inm.Superficie)}, @{nameof(inm.Latitud)},
                               @{nameof(inm.Longitud)},@{nameof(inm.Precio)} ,@{nameof(inm.Tipo)},@{nameof(inm.Uso)});
                              SELECT LAST_INSERT_ID();";

                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    
                    comm.Parameters.AddWithValue($"@{nameof(inm.Direccion)}", inm.Direccion);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Ambientes)}", inm.Ambientes);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Superficie)}",inm.Superficie);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Latitud)}", inm.Latitud);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Longitud)}",inm.Longitud);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Precio)}", inm.Precio);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Tipo)}", inm.Tipo);
                    comm.Parameters.AddWithValue($"@{nameof(inm.Uso)}", inm.Uso);

                    
                   
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    inm.Id = res;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM Inmuebles WHERE Id = @id";
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
        public int Modificacion(Inmueble inm)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"UPDATE Inmuebles SET Direccion=@direccion, Ambientes=@ambientes, Superficie=@superficie, Latitud=@latitud, Longitud=@longitud, Precio=@precio, Tipo=@tipo, Uso=@uso " +
                    $"WHERE Id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@direccion", inm.Direccion);
                    command.Parameters.AddWithValue("@ambientes", inm.Ambientes);
                    command.Parameters.AddWithValue("@superficie", inm.Superficie);
                    command.Parameters.AddWithValue("@latitud", inm.Latitud);
                    command.Parameters.AddWithValue("@longitud", inm.Longitud);
                    command.Parameters.AddWithValue("@precio", inm.Precio);
                    command.Parameters.AddWithValue("@tipo", inm.Tipo);
                    command.Parameters.AddWithValue("@uso", inm.Uso);                    
                    command.Parameters.AddWithValue("@id", inm.Id);
                

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
             public IList<Inmueble> ObtenerTodos()
        {
            var res = new List<Inmueble>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"SELECT Id,Direccion,Ambientes,Superficie,Latitud,Longitud, Precio, Tipo, Uso
                        FROM Inmuebles;";
                        
             using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var inm = new Inmueble
                        {
                            Id = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetDecimal(4),
                            Longitud = reader.GetDecimal(5),
                            Precio = reader.GetDecimal(6),
                            Tipo = reader.GetString(7),
                            Uso = reader.GetString(8),
                            
                           

                        };
                        res.Add(inm);
                    }
                    conn.Close();
                }
                
            }
            return res;
            
        }

        public Inmueble ObtenerPorId(int id)
        {
            Inmueble inm = null;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT Id, Direccion, Ambientes, Superficie, Latitud, Longitud, Precio, Tipo, Uso FROM Inmuebles" +
                    $" WHERE Id=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inm = new Inmueble
                        {
                            Id = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetDecimal(4),
                            Longitud = reader.GetDecimal(5), 
                            Precio = reader.GetDecimal(6),
                            Tipo = reader.GetString(7),
                            Uso = reader.GetString(8),      
                        };
                    }
                    connection.Close();
                }
            }
            return inm;

        }

    }
}
    