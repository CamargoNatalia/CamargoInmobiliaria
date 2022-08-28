using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria;

 public class RepositorioInquilino
    {

        string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        public RepositorioInquilino()
        {

        }
        public IList<Inquilino> obtenerTodos()
        {
            var res = new List<Inquilino>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email
                        FROM Inquilinos;";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var i = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            
                           
                        };
                        res.Add(i);
                    }
                    conn.Close();
                }
                return res;
            }

        }

        public int Alta(Inquilino i)
        {
            var res = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"INSERT INTO Inquilinos (Nombre,Apellido,Dni,Telefono,Email)
                               VALUES (@{nameof(i.Nombre)},@{nameof(i.Apellido)},@{nameof(i.Dni)},@{nameof(i.Telefono)},@{nameof(i.Email)});" +
                              "SELECT LAST_INSERT_ID();";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(i.Nombre)}", i.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(i.Apellido)}", i.Apellido);
                    comm.Parameters.AddWithValue($"@{nameof(i.Dni)}", i.Dni);
                    comm.Parameters.AddWithValue($"@{nameof(i.Telefono)}", i.Telefono);
                    comm.Parameters.AddWithValue($"@{nameof(i.Email)}", i.Email);
                    
                   
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    i.Id = res;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM Inquilinos WHERE Id = @id";
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
        public int Modificacion(Inquilino i)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"UPDATE Inquilinos SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email " +
                    $"WHERE Id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@id", i.Id);
                    
                    

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Inquilino ObtenerPorId(int id)
        {
            Inquilino i = null;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos" +
                    $" WHERE Id=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre =  reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            
                            
                        };
                    }
                    connection.Close();
                }
            }
            return i;

        }
    }