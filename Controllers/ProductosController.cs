using Microsoft.AspNetCore.Mvc;
using models;

[ApiController]
[Route("[controller]")]
public class ProductosController : Controller
{
    private readonly ProductosRepository _prodRepository;

    public ProductosController()
    {
        _prodRepository = new ProductosRepository();
    }

    [HttpGet("ListarProductos")]
    public IActionResult ListarProductos()
    {
        return View(_prodRepository.ListarProductos());
    }

    public IActionResult CrearProducto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearProducto([FromForm] Producto producto)
    {
        if (ModelState.IsValid)
        {
            _prodRepository.CrearProducto(producto);
            return RedirectToAction("ListarProductos");
        }
        return View(producto);
    }

    [HttpGet("ModificarProducto/{id}")]
    public IActionResult ModificarProducto(int id)
    {
        var producto = _prodRepository.ObtenerDetallePorId(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    [HttpPost("ModificarProducto/{id}")]
    public IActionResult ModificarProducto(int id, [FromForm] Producto producto)
    {
        if (ModelState.IsValid)
        {
            _prodRepository.ModificarProducto(id, producto);
            return RedirectToAction("ListarProductos");
        }
        return View(producto);
    }

    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int id)
    {
        var producto = _prodRepository.ObtenerDetallePorId(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    [HttpGet("EliminarProducto/{id}")]
    public IActionResult EliminarProducto(int id)
    {
        _prodRepository.EliminarProducto(id);
        return RedirectToAction("ListarProductos");
    }


    
}
