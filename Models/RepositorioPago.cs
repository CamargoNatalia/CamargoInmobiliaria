using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria{

    public class RepositorioPago
    {

    string connectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    public RepositorioPago (){
    }
     public int Alta(Pago pago)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @$"INSERT INTO Pagos (nroPago,fechaPago, Importe,c.id) 
					VALUES (@{nameof(pago.nroPago)},(@{nameof(pago.ContratoId)}, (@{nameof(pago.fechaPago)}, (@{nameof(pago.Importe)});
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue($"@{nameof(pago.nroPago)}", pago.nroPago);
					command.Parameters.AddWithValue($"@{nameof(pago.ContratoId)}", pago.ContratoId);
					command.Parameters.AddWithValue($"@{nameof(pago.fechaPago)}", pago.fechaPago);
					command.Parameters.AddWithValue($"@{nameof(pago.Importe)}", pago.Importe);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
                    pago.id = res;
                    connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Pagos WHERE id = @id";
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
		public int Modificacion(Pago pago)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE Pagos SET nroPago = nroPago, fechaPago=@fechaPago, Importe=@importe, ContratoId=@contratoId " +
					$"WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nroPago", pago.nroPago);
					command.Parameters.AddWithValue("@fechaPago", pago.fechaPago);
					command.Parameters.AddWithValue("@importe", pago.Importe);
                    command.Parameters.AddWithValue("@contratoId", pago.ContratoId);
				
					command.Parameters.AddWithValue("@id", pago.id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Pago> ObtenerTodos()
		{
			IList<Pago> res = new List<Pago>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, nroPago, fechaPago, Importe, ContratoId FROM Pagos";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pago p = new Pago
						{
							id = reader.GetInt32(0),
                            ContratoId = reader.GetInt32(1),
							nroPago = reader.GetInt32(2),
                            fechaPago = reader.GetDateTime(3),
                            Importe = reader.GetFloat(4),
                            c = new Contrato{
                                Id = reader.GetInt32(1),
                                fechaInicio = reader.GetDateTime(5),
                                fechaFin = reader.GetDateTime(6),
                            }
						};
                             
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}
    virtual public Pago ObtenerPorId(int id)
		{
			Pago p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, nroPago, fechaPago, Importe, ContratoId FROM Pagos" +
					$" WHERE id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", MySqlDbType.Int24).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Pago
						{
							id = reader.GetInt32(0),
							nroPago = reader.GetInt32(1),
                            fechaPago = reader.GetDateTime(2),
                            Importe = reader.GetFloat(3),
							ContratoId = reader.GetInt32(4),
							 c = new Contrato{
                                Id = reader.GetInt32(1),
                                fechaInicio = reader.GetDateTime(5),
                                fechaFin = reader.GetDateTime(6),
                            }
						};
					}
					connection.Close();
				}
			}
			return p;
        }
		virtual public Pago ObtenerPorContrato(int id)
		{
			Pago p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, ContratoId, nroPago, fechaPago, Importe FROM Pagos" +
					$" WHERE ContratoId=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", MySqlDbType.Int24).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Pago
						{
							id = reader.GetInt32(0),
                            ContratoId = reader.GetInt32(1),
							nroPago = reader.GetInt32(2),
                            fechaPago = reader.GetDateTime(3),
                            Importe = reader.GetFloat(4),
						};
					}
					connection.Close();
				}
			}
			return p;
        }
		
	}
    }
