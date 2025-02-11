namespace models;

public class AgregarProductoAPresupuesto
{
    int idProducto;
    int cantidad;
    List<Producto> productos;

    public int IdProducto { get => idProducto; set => idProducto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public List<Producto> Productos { get => productos; set => productos = value; }

    public AgregarProductoAPresupuesto(List<Producto> productos)
    {
        IdProducto=0;
        Cantidad=0;
        Productos=productos;
    }

    public AgregarProductoAPresupuesto()
    {
        IdProducto=0;
        Cantidad=0;
        Productos=new List<Producto>();
    }
}