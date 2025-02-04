namespace models;
public class Producto
{
    int idProducto;
    string descripcion;
    int precio;

    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }

    //Constructor
    public Producto(int idProducto, string descripcion, int precio)
    {
        IdProducto=idProducto;
        Descripcion=descripcion;
        Precio=precio;
    }
    public Producto()
    {

    }
}