using System.ComponentModel.DataAnnotations;
namespace models;

public class ProductoViewModel{
    string descripcion;
    double precio;

    public ProductoViewModel(string descripcion, int precio)
    {
        this.Descripcion = descripcion;
        this.Precio = precio;
    }

    [StringLength(250)]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    [Required(ErrorMessage ="Error al cargar el precio")]
    [Range(0.01, double.MaxValue)]
    public double Precio { get => precio; set => precio = value; }
}