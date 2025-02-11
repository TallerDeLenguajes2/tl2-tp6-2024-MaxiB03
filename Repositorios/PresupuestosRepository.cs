using Microsoft.Data.Sqlite;
using models;

public class PresupuestosRepository
{
    string connectionString = "Data Source=DB/Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuestos nuevoPresupuesto)
    {
        string query = "INSERT INTO Presupuestos (FechaCreacion, ClienteId) VALUES (@Fecha, @idCli)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@Fecha", nuevoPresupuesto.FechaCreacion);
            command.Parameters.AddWithValue("@idCli", nuevoPresupuesto.Cliente.IdCliente);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Presupuestos ObtenerPresupuesto(int idPres)
    {
        Presupuestos? presupuesto = null;
        string query = "SELECT * FROM Presupuestos INNER JOIN Clientes USING(ClienteId) WHERE idPresupuesto = @idPres";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@idPres", idPres);

            // Se utiliza para ejecutar una consulta SQL y obtener un lector de datos (SqliteDataReader) que puedes usar para recorrer los resultados.
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                if(reader.Read()) // Verifica si hay filas
                {
                    // Accede a las columnas usando índices o nombres (reader[0] o reader["Columna"]).
                    Cliente c = new Cliente
                    (
                        Convert.ToInt32(reader["ClienteId"]),
                        Convert.ToString(reader["Nombre"]),
                        Convert.ToString(reader["Email"]),
                        Convert.ToString(reader["Telefono"])
                    );

                    presupuesto = new Presupuestos
                    (
                        Convert.ToInt32(reader["idPresupuesto"]),
                        c,
                        Convert.ToString(reader["FechaCreacion"])
                    );
                }
            }
            connection.Close();
        }

        return presupuesto;
    }

    public List<Presupuestos> ListarPresupuestos()
    {
        List <Presupuestos> presupuestos = new List<Presupuestos>();
        string query = "SELECT * FROM Presupuestos INNER JOIN Clientes USING(ClienteId)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Cliente c = new Cliente
                    (
                        Convert.ToInt32(reader["ClienteId"]),
                        Convert.ToString(reader["Nombre"]),
                        Convert.ToString(reader["Email"]),
                        Convert.ToString(reader["Telefono"])
                    );

                    Presupuestos presupuesto = new Presupuestos
                    (
                        Convert.ToInt32(reader["idPresupuesto"]),
                        c,
                        Convert.ToString(reader["FechaCreacion"])
                    );
                    presupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return presupuestos;
    }

    public void ModificarPresupuesto(int idPres, Presupuestos presupuesto)
    {
        string query = @"UPDATE Presupuestos
                        SET FechaCreacion = @fecha
                        WHERE idPresupuesto = @idPres";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            connection.Open();

            command.Parameters.AddWithValue("@fecha", presupuesto.FechaCreacion);
            command.Parameters.AddWithValue("@idPres", idPres);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

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

    public void AgregarPresupuesto(int idPresupuesto, int idProducto, int cant)
    {
        var query = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                    VALUES ((SELECT idPresupuesto FROM Presupuestos WHERE idPresupuesto = @pres),
                            (SELECT idProducto FROM Productos WHERE idProducto = @prod), @cant)";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@pres", idPresupuesto);
            command.Parameters.AddWithValue("@prod", idProducto);
            command.Parameters.AddWithValue("@cant", cant);
            
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