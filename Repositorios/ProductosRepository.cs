using Microsoft.Data.Sqlite;
using models;

public class ProductosRepository
{
    string connectionString = "Data Source=DB/Tienda.db;Cache=Shared";

    public void CrearProducto(Producto producto)
    {
        string query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);
            
            connection.Open();

            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void ModificarProducto(int idProd, Producto producto)
    {
        string query = @"UPDATE Productos
                        SET Descripcion = @Descripcion, Precio = @Precio
                        WHERE idProducto = @IdProd";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@IdProd", idProd);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>();
        string query = "SELECT * FROM Productos";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                // Usa el método Read() para avanzar a la siguiente fila.
                while(reader.Read())
                {
                    var producto= new Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);

                    productos.Add(producto);
                }
            }
            connection.Close();
        }

        return productos;
    }

    public Producto ObtenerDetallePorId(int idProd)
    {
        Producto? producto = null;
        string query = "SELECT * FROM Productos WHERE idProducto = @idProd";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@idProd", idProd);

            // Se utiliza para ejecutar una consulta SQL y obtener un lector de datos (SqliteDataReader) que puedes usar para recorrer los resultados.
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                if(reader.Read()) // Verifica si hay filas
                {
                    // Accede a las columnas usando índices o nombres (reader[0] o reader["Columna"]).
                    producto = new Producto 
                    {
                        IdProducto = Convert.ToInt32(reader["idProducto"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToInt32(reader["Precio"])
                    };
                }
            }
            connection.Close();
        }

        return producto;
    }

    public void EliminarProducto(int idProd)
    {
        string query = "DELETE FROM Productos WHERE idProducto = @IdProducto";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();
            command.Parameters.AddWithValue("@IdProducto", idProd);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}