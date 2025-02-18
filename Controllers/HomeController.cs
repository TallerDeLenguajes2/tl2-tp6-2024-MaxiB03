using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using models;
using tl2_tp6_2024_MaxiB03.Models;

namespace tl2_tp6_2024_MaxiB03.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction ("Index", "Login");
        return View();
    }

    public IActionResult Privacy()
    {
        if (!IsAuthenticated()) return RedirectToAction("Index", "Login");
        if(GetAccessLevel() == AccessLevel.Admin) return RedirectToAction("Index", "Home");
        return View();
    }

    protected bool IsAuthenticated()
    {
        bool salida;
        var exito = bool.TryParse(HttpContext.Session.GetString("IsAuthenticated"), out salida);
        return salida;
    }

    protected AccessLevel GetAccessLevel()
    {
        return (AccessLevel)Enum.Parse(typeof(AccessLevel), HttpContext.Session.GetString("AccessLevel"));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
