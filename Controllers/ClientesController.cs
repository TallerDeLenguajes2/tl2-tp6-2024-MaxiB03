using Microsoft.AspNetCore.Mvc;
using models;

[ApiController]
[Route("[controller]")]
public class ClientesController : Controller
{
    private readonly ClienteRepository _clienteRepository;

    public ClientesController()
    {
        _clienteRepository = new ClienteRepository();
    }

    [HttpGet("Crear")]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost("CrearCliente")]
    public IActionResult CrearCliente([FromForm]Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _clienteRepository.CrearCliente(cliente);
            return RedirectToAction("ListarClientes");
        }
        return View(cliente);
    }

    [HttpGet("ListarClientes")]
    public IActionResult ListarClientes()
    {
        return View(_clienteRepository.ListarClientes());
    }

    [HttpGet("Modificar")]
    public IActionResult Modificar(int id)
    {
        var cliente = _clienteRepository.ObtenerCliente(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    [HttpPost("ModificarCliente/{id}")]
    public IActionResult ModificarCliente(int id, [FromForm] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _clienteRepository.ModificarCliente(id, cliente);
            return RedirectToAction("ListarClientes");
        }
        return View(cliente);
    }

    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int id)
    {
        var cliente = _clienteRepository.ObtenerCliente(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    [HttpGet("EliminarCliente/{id}")]
    public IActionResult EliminarCliente(int id)
    {
        _clienteRepository.EliminarCliente(id);
        return RedirectToAction("ListarClientes");
    }
}
