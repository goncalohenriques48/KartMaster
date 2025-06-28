using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KartMaster.Models;

namespace KartMaster.Controllers;

/// <summary>
/// Controlador principal que gere as páginas iniciais e institucionais.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Construtor que injeta o logger do controlador.
    /// </summary>
    /// <param name="logger">Instância de ILogger.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Página inicial da aplicação.
    /// </summary>
    /// <returns>Vista da página inicial.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Página de política de privacidade.
    /// </summary>
    /// <returns>Vista com informações de privacidade.</returns>
    public IActionResult Privacy()
    {
        return View();
    }
    /// <summary>
    /// Página "Sobre" com informações institucionais ou da equipa.
    /// </summary>
    /// <returns>Vista com a secção "Sobre".</returns>
    public IActionResult Sobre() {
        return View();
    }

    /// <summary>
    /// Página de erro apresentada em caso de exceções não tratadas.
    /// </summary>
    /// <returns>Vista com detalhes do erro.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
