using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria{

public class RepositorioPago
{   
   string ConnectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";


   public IList<Pago> ObtenerTodos()
   {
     var res = new List<Pago>();
    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
    {
        string sql = @"SELECT p.Id, p.FechaPago, p.Importe, p.ContratoId, fechaInicio FROM pagos p INNER JOIN contratos c WHERE p.ContratoId = c.Id";
        
        using (MySqlCommand command = new MySqlCommand(sql, conn))
        {
            conn.Open();
            var reader = command.ExecuteReader();
        
               while (reader.Read())
               {
                    var pago= new Pago
                    {
                    Id = reader.GetInt32(0),
                    nroPago = reader.GetInt32(1),
                    fechaPago = reader.GetDateTime(2),
                    importe = reader.GetDecimal(3),
                    ContratoId = reader.GetInt32(4),
                    contrato = new Contrato
                    {
                        fechaInicio = reader.GetDateTime(nameof(Contrato.fechaInicio))
                    }
                    
                    };  
                 res.Add(pago);
                }
        conn.Close();
    }
    return res;
   }
   }
         

}
}