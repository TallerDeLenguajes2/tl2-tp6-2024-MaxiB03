using Microsoft.Data.Sqlite;
using models;

public class PresupuestosRepository
{
    string connectionString = "Data Source=DB/Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuestos nuevoPresupuesto)
    {
        string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@Nombre, @Fecha)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@Nombre", nuevoPresupuesto.NombreDestinatario);
            command.Parameters.AddWithValue("@Fecha", nuevoPresupuesto.FechaCreacion);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Presupuestos> ListarPresupuestos()
    {
        List <Presupuestos> presupuestos = new List<Presupuestos>();
        string query = "SELECT * FROM Presupuestos";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Presupuestos presupuesto = new Presupuestos();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = reader["FechaCreacion"].ToString();
                    presupuesto.Detalles = ObtenerDetallePresupuesto(presupuesto.IdPresupuesto);
                    
                    presupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return presupuestos;
    }

    /*private Producto ObtenerProductoPorId(int idProducto)
    {
        Producto producto = null;

        string query = "SELECT * FROM Productos WHERE IdProducto = @IdProducto";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@IdProducto", idProducto);

            connection.Open();

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
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
    }*/

    public List<PresupuestosDetalle> ObtenerDetallePresupuesto(int idPresupuesto)
    {
        List<PresupuestosDetalle> detalles = new List<PresupuestosDetalle>();
        string query = @"
                    SELECT 
                        pd.idProducto, 
                        pd.Cantidad, 
                        p.Descripcion, 
                        p.Precio
                    FROM PresupuestosDetalle pd
                    INNER JOIN Productos p ON pd.idProducto = p.idProducto
                    WHERE pd.idPresupuesto = @idPresupuesto";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);

            connection.Open();

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PresupuestosDetalle detalle = new PresupuestosDetalle
                    {
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToInt32(reader["Precio"])
                        }
                    };

                    detalles.Add(detalle);
                }
            }
            
            connection.Close();
        }

        return detalles;
    }

    public void AgregarDetalleAlPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@IdPresupuesto, @IdProducto, @Cantidad)";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);
            command.Parameters.AddWithValue("@IdProducto", idProducto);
            command.Parameters.AddWithValue("@Cantidad", cantidad);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void EliminarPresupuesto(int idPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
             connection.Open();

            //Elimino primero los detalles relacionados con el presupuesto
            string deleteDetallesQuery = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
            using (SqliteCommand command = new SqliteCommand(deleteDetallesQuery, connection))
            {
                command.Parameters.AddWithValue("@id", idPresupuesto);
                command.ExecuteNonQuery();
            }

            //Luego elimino el presupuesto
            string deletePresupuestoQuery = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
            using (SqliteCommand command = new SqliteCommand(deletePresupuestoQuery, connection))
            {
                command.Parameters.AddWithValue("@id", idPresupuesto);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}