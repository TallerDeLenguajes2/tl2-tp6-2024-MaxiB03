using Microsoft.AspNetCore.Mvc;
using models;

public class LoginController : Controller
{
    private ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        var model = new LoginViewModel
        {
            IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true"
        };

        if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
        {
            ViewBag.ErrorMessage = "La sesión ha expirado. Por favor, inicie sesión nuevamente.";
        }
        
        return View(model);
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        try
        {
            //Verifico que los datos de entrada no esten vacios
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                model.ErrorMessage = "Por favor ingrese su nombre de usuario y contraseña.";
                return View("Index", model);
            }

            Usuario usuario = _usuarioRepository.GetUser(model.Username, model.Password);

            //Si el usuario existe
            if(usuario != null)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("User", usuario.Username);
                HttpContext.Session.SetString("AccessLevel", usuario.AccessLevel1.ToString());

                _logger.LogInformation("El usuario: "+ usuario.Username+" Ingresó correctamente");
                return RedirectToAction("Index", "Home");
            }

            // Logueo de intento fallido
            _logger.LogWarning($"Intento de acceso inválido - Usuario: {model.Username} Clave ingresada: {model.Password}");
            model.ErrorMessage = "Credenciales Inválidas";
            model.IsAuthenticated = false;

            return View("Index", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se puso autenticar el usuario";
            return View("Index", model);
        }
    }

    public IActionResult Logout()
    {
        try
        {
            // Limpio la sesión
            HttpContext.Session.Clear();

            // Redirigir a la vista de login
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo desloguear el usuario";
            return View("Index");
        }
    }
}