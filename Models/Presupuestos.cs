namespace models;

public class Presupuestos
{
    int idPresupuesto;
    string nombreDestinatario;
    string fechaCreacion;
    List<PresupuestosDetalle> detalles;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle> Detalles { get => detalles; set => detalles = value; }

    //Constructor
    public Presupuestos(int idPresupuesto, string nombreDestinatario, string fechaCreacion, List<PresupuestosDetalle> detalles)
    {
        IdPresupuesto=idPresupuesto;
        NombreDestinatario=nombreDestinatario;
        FechaCreacion=fechaCreacion;
        Detalles=detalles;
    }

    public Presupuestos(int idPresupuesto, string nombreDestinatario, string fechaCreacion)
    {
        IdPresupuesto=idPresupuesto;
        NombreDestinatario=nombreDestinatario;
        FechaCreacion=fechaCreacion;
        Detalles=new List<PresupuestosDetalle>();
    }

    public Presupuestos(){}
}