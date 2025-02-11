using Microsoft.AspNetCore.Mvc;
using models;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository _presupuestoRepository;
    private readonly ClienteRepository _clienteRepository;
    private readonly ProductosRepository _productoRepository;

    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestosRepository();
        _clienteRepository = new ClienteRepository();
        _productoRepository = new ProductosRepository();
    }

    [HttpGet("Crear")]
    public IActionResult Crear()
    {
        var clientes=_clienteRepository.ListarClientes();
        var viewModel = new CrearPresupuestoViewModel(clientes);
        return View(viewModel);
    }

    [HttpGet("Agregar/{idPresupuesto}")]
    public IActionResult Agregar(int idPresupuesto)
    {
        var productos = _productoRepository.ListarProductos();
        var viewModel = new AgregarProductoAPresupuesto(productos);
        ViewBag.IdPresupuesto = idPresupuesto;

        return View(viewModel);
    }

    [HttpPost("AgregarProducto/{idPres}")]
    public IActionResult AgregarProducto([FromRoute]int idPres, [FromForm] int idProducto, [FromForm] int Cantidad)
    {
        Console.WriteLine($"ID Presupuesto recibido: {idPres}");
        _presupuestoRepository.AgregarPresupuesto(idPres,idProducto,Cantidad);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpPost("CrearPresupuesto")]
    public IActionResult CrearPresupuesto([FromForm] int idCliente)
    {
        if (!ModelState.IsValid)
        {
            return View("Crear");
        }

        Presupuestos nuevoPresupuesto = new Presupuestos();
        Cliente cliente = _clienteRepository.ObtenerCliente(idCliente);

        nuevoPresupuesto.Cliente = cliente;
        nuevoPresupuesto.FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd");
        nuevoPresupuesto.Detalles = new List<PresupuestosDetalle>();

        _presupuestoRepository.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("Modificar")]
    public IActionResult Modificar(int id)
    {
        return View(_presupuestoRepository.ObtenerPresupuesto(id));
    }

    [HttpPost("ModificarPresupuesto")]
    public IActionResult ModificarPresupuesto([FromForm]Presupuestos nuevoPresupuesto)
    {
        _presupuestoRepository.ModificarPresupuesto(nuevoPresupuesto.IdPresupuesto,nuevoPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("ListarPresupuestos")]
    public IActionResult ListarPresupuestos()
    {
        return View(_presupuestoRepository.ListarPresupuestos());
    }

    [HttpGet("Detalle")]
    public IActionResult Detalle(int id)
    {
        return View (_presupuestoRepository.ObtenerDetallePresupuesto(id));
    }
}
