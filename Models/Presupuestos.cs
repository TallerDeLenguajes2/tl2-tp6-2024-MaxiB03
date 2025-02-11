namespace models;

public class Presupuestos
{
    int idPresupuesto;
    Cliente cliente;
    string fechaCreacion;
    List<PresupuestosDetalle> detalles;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle> Detalles { get => detalles; set => detalles = value; }

    //Constructor
    public Presupuestos(int idPresupuesto, Cliente cliente, string fechaCreacion, List<PresupuestosDetalle> detalles)
    {
        IdPresupuesto=idPresupuesto;
        this.Cliente=cliente;
        FechaCreacion=fechaCreacion;
        Detalles=detalles;
    }

    public Presupuestos(int idPresupuesto, Cliente cliente, string fechaCreacion)
    {
        IdPresupuesto=idPresupuesto;
        this.Cliente=cliente;
        FechaCreacion=fechaCreacion;
        Detalles=new List<PresupuestosDetalle>();
    }

    public Presupuestos(){}
}