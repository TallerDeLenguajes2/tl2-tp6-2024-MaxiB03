namespace models;

public class PresupuestosDetalle
{
    int cantidad;
    Producto producto;

    public int Cantidad { get => cantidad; set => cantidad = value; }
    public Producto Producto { get => producto; set => producto = value; }

    //Constructor
    public PresupuestosDetalle(int cantidad, Producto producto)
    {
        Cantidad=cantidad;
        Producto=producto;
    }

    public PresupuestosDetalle(){}
}