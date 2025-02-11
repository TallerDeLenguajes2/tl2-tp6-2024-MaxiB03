namespace models;

public class CrearPresupuestoViewModel
{
    int idCliente;
    string fechaCreacion;
    List<Cliente> clientes;

    public int IdCliente { get => idCliente; set => idCliente = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<Cliente> Clientes { get => clientes; set => clientes = value; }

    public CrearPresupuestoViewModel()
    {
        IdCliente=0;
        FechaCreacion=string.Empty;
        Clientes=new List<Cliente>();
    }

    public CrearPresupuestoViewModel(List<Cliente> clientes)
    {
        IdCliente=0;
        FechaCreacion=string.Empty;
        this.clientes=clientes;
    }
}