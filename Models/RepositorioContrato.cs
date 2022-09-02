using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria;

public class RepositorioContrato{

    string ConnectionString = "Server= localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioContrato()
    {
        
    }

    public int Alta(Contrato contrato){

        int res = -1;

        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
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
					contrato.InmuebleId = res;
                    contrato.InquilinoId = res;
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
				string sql = $"DELETE FROM Contartos WHERE Id = {id}";
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
		public int Modificacion(Contrato contrato)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
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
}

