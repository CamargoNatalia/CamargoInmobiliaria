using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria{

 public class RepositorioContrato
    {

        string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        

        public int Alta(Contrato c)
        {
            var res = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"INSERT INTO Contratos (fechaInicio,fechaFinal,montoMensual)
                               VALUES (@{nameof(c.fechaInicio)},@{nameof(c.fechaFinal)},@{nameof(c.montoMensual)}); 
                              SELECT LAST_INSERT_ID();";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
					
                    comm.Parameters.AddWithValue($"@{nameof(c.fechaInicio)}", c.fechaInicio);
                    comm.Parameters.AddWithValue($"@{nameof(c.fechaFinal)}", c.fechaFinal);
                    comm.Parameters.AddWithValue($"@{nameof(c.montoMensual)}", c.montoMensual);
                    
                   
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    c.Id = res;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM contratos WHERE Id = @id";
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
        public int Modificacion(Contrato c)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"UPDATE Contratos SET fechaInicio=@fechaInicio, fechaFinal=@fechaFinal, montoMensual=@montoMensual " +
                    $"WHERE Id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@fechaInicio", c.fechaInicio);
                    command.Parameters.AddWithValue("@fechaFinal", c.fechaFinal);
                    command.Parameters.AddWithValue("@montoMensual", c.montoMensual);
                    command.Parameters.AddWithValue("@id", c.Id);
                

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }


        public Contrato ObtenerPorId(int id)
        {
            Contrato c = null;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT Id, fechaInicio, fechaFinal, montoMensual FROM Contratos" +
                    $" WHERE Id=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
                        c = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            fechaInicio = reader.GetDateTime(1),
                            fechaFinal = reader.GetDateTime(2),
                            montoMensual = reader.GetDecimal(3),
                                     
                        };
                    }
                    connection.Close();
                }
            }
            return c;

        }

         public IList<Contrato> ObtenerTodos()
        {
            var res = new List<Contrato>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"SELECT Id,fechaInicio,fechaFinal,montoMensual
                        FROM Contratos;";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var c = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            fechaInicio = reader.GetDateTime(1),
                            fechaFinal = reader.GetDateTime(2),
                            montoMensual = reader.GetDecimal(3),
                            
                            
                           
                        };
                        res.Add(c);
                    }
                    conn.Close();
                }
                return res;
            }

        }


    }
}

