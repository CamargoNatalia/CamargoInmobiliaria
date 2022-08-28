using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria;

public class RepositorioPropietario
    {
        string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

        public RepositorioPropietario()
        {

        }

        public IList<Propietario> obtenerTodos()
        {
            var res = new List<Propietario>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email
                        FROM Propietarios;";
             using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var p = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                           

                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
                return res;
            }
            
        }

        public int Alta(Propietario p)
        {
            var res = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"INSERT INTO Propietarios (Nombre,Apellido,Dni,Telefono,Email)
                               VALUES(@{nameof(p.Nombre)},@{nameof(p.Apellido)},@{nameof(p.Dni)},@{nameof(p.Telefono)},@{nameof(p.Email)});
                                SELECT LAST_INSERT_ID();";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(p.Nombre)}", p.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(p.Apellido)}", p.Apellido);
                    comm.Parameters.AddWithValue($"@{nameof(p.Dni)}", p.Dni);
                    comm.Parameters.AddWithValue($"@{nameof(p.Telefono)}", p.Telefono);
                    comm.Parameters.AddWithValue($"@{nameof(p.Email)}", p.Email);
                    
                    conn.Open(); 
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    p.Id = res;
                    
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM Propietarios WHERE Id = @id";
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
        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"UPDATE Propietarios SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email " +
                    $"WHERE Id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@email", p.Email);
                    command.Parameters.AddWithValue("@id", p.Id);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Propietario ObtenerPorId(int id)
        {
            Propietario p = null;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email FROM Propietarios" +
                    $" WHERE Id=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),         
                        };
                    }
                    connection.Close();
                }
            }
            return p;

        }
    }


    