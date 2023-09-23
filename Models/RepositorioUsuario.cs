using System.Data;
using MySql.Data.MySqlClient;

namespace CamargoInmobiliaria{

public class RepositorioUsuario
{   
   string ConnectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

   public RepositorioUsuario()
   {
    
   }

   public IList<Usuario> ObtenerTodos()
   {
   var res = new List<Usuario>();
    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
    {
       string sql = @"SELECT Id, Nombre, Email, IdRol FROM usuarios;";
        
        
        using (MySqlCommand comm = new MySqlCommand(sql, conn))
        {
            conn.Open();

           var reader = comm.ExecuteReader();           
               while (reader.Read())
               {
                var user = new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Email = reader.GetString(2),
                    IdRol = reader.GetInt32(3),
                };
                res.Add(user);
               }         
        }
        conn.Close();

    }
    return res;
   }

   public int Alta(Usuario user)
   {
    int res = -1;

    using(MySqlConnection conn = new MySqlConnection(ConnectionString))
    {   
        string sql = @$"INSERT INTO usuarios(Nombre, Email, Password, IdRol) 
        VALUES (@{nameof(user.Nombre)},@{nameof(user.Email)},@{nameof(user.Password)},@{nameof(user.IdRol)});
         SELECT LAST_INSERT_ID();";
        
       using (MySqlCommand comm = new MySqlCommand(sql, conn))
        {
            comm.Parameters.AddWithValue($"@{nameof(user.Nombre)}", user.Nombre);
            comm.Parameters.AddWithValue($"@{nameof(user.Email)}", user.Email);
            comm.Parameters.AddWithValue($"@{nameof(user.Password)}", user.Password);
            comm.Parameters.AddWithValue($"@{nameof(user.IdRol)}", user.IdRol);
                   
            conn.Open();
            res = Convert.ToInt32(comm.ExecuteScalar());
            user.Id = res;
            conn.Close();
        }
    }

    return res;
   }

   
    public int Baja(int id)
    {
        int res = -1;
        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            string sql = @$"DELETE FROM usuarios WHERE Id = @id";

            using(MySqlCommand command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                command.CommandType = CommandType.Text;
                conn.Open();
                res = command.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }

    public int Modificar(Usuario usuario)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				string sql = @$"UPDATE usuarios SET Nombre=@Nombre,  Email=@Email, Password=@Password WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
					command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Password", usuario.Password);
					command.Parameters.AddWithValue("@id", usuario.Id);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}


         public Usuario ObtenerPorId(int id)
    {
        Usuario usuario = null;
        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            string sql = @$"SELECT Id, Nombre, Email, Password, IdRol FROM usuarios WHERE Id = @id";

            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                command.CommandType = CommandType.Text;
                conn.Open();
                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    usuario = new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Email = reader.GetString(2),
                        Password = reader.GetString(3),
                        IdRol = reader.GetInt32(4)
                    };

                }
                conn.Close();
            }
        }
        return usuario;
    }
    }
}
