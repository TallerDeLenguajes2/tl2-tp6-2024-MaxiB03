using Microsoft.Data.Sqlite;
using models;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string connectionString;

    public UsuarioRepository(string CadenaDeConexion)
    {
        connectionString = CadenaDeConexion;
    }

    public Usuario GetUser(string username, string password)
    {
        Usuario user = null;

        string query = @"SELECT * FROM Usuarios WHERE Usuario = @username AND Password = @contra ";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query,connection);

            connection.Open();
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@contra", password);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new Usuario();
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Nombre = reader["Nombre"].ToString();
                    user.Username = reader["Usuario"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.AccessLevel1 = (AccessLevel)Convert.ToInt32(reader["Rol"]);;
                }

            }
            connection.Close();            
        }
        return user;
    }

    public void AltaUsuario(Usuario usuario)
    {
        string query = @"INSERT INTO Usuario (Nombre, Usuario, Password, Rol) VALUES (@nombre, @usu, @contra, @rol)";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query,connection);

            connection.Open();

            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@usu", usuario.Username);
            command.Parameters.AddWithValue("@contra", usuario.Password);
            command.Parameters.AddWithValue("@rol", (int)usuario.AccessLevel1);
            command.ExecuteNonQuery();

            connection.Close();            
        }
    }

}