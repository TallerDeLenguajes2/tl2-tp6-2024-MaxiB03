using Microsoft.Data.Sqlite;
using models;

public class ClienteRepository
{
    string connectionString = "Data Source=DB/Tienda.db;Cache=Shared";

    public void CrearCliente(Cliente cliente)
    {
        string query = "INSERT INTO Clientes (Nombre,Email,Telefono) VALUES (@nom, @email,@tel)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);
            
            connection.Open();

            command.Parameters.AddWithValue("@nom", cliente.Nombre);
            command.Parameters.AddWithValue("@email", cliente.Email);
            command.Parameters.AddWithValue("@tel", cliente.Telefono);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void ModificarCliente(int idCliente, Cliente cliente)
    {
        string query = @"UPDATE Clientes
                        SET Nombre = @nombre, Email = @email, Telefono = @tel
                        WHERE ClienteId = @idCli";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@email", cliente.Email);
            command.Parameters.AddWithValue("@tel", cliente.Telefono);
            command.Parameters.AddWithValue("@idCli", idCliente);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Cliente> ListarClientes()
    {
        List<Cliente> clientes = new List<Cliente>();
        string query = "SELECT * FROM Clientes";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                // Usa el método Read() para avanzar a la siguiente fila.
                while(reader.Read())
                {
                    var cliente= new Cliente();
                    cliente.IdCliente = Convert.ToInt32(reader["ClienteId"]);
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();

                    clientes.Add(cliente);
                }
            }
            connection.Close();
        }
        return clientes ?? new List<Cliente>();
    }

    public Cliente ObtenerCliente(int idCliente)
    {
        Cliente? cliente=null;
        string query = "SELECT * FROM Clientes WHERE ClienteId = @idCli";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@idCli", idCliente);

            // Se utiliza para ejecutar una consulta SQL y obtener un lector de datos (SqliteDataReader) que puedes usar para recorrer los resultados.
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                if(reader.Read()) // Verifica si hay filas
                {
                    // Accede a las columnas usando índices o nombres (reader[0] o reader["Columna"]).
                    cliente = new Cliente 
                    {
                        IdCliente = Convert.ToInt32(reader["ClienteId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                }
            }
            connection.Close();
        }

        return cliente;
    }

    public void EliminarCliente(int idCliente)
    {
        string query = "DELETE FROM Clientes WHERE ClienteId = @idCli";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();
            command.Parameters.AddWithValue("@idCli", idCliente);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}