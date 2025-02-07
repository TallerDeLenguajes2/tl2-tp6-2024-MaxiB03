using Microsoft.AspNetCore.Mvc;
using models;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository _presupuestoRepository;

    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestosRepository();
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
