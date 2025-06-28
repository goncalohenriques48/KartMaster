using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KartMaster.Models;

namespace KartMaster.Controllers;

/// <summary>
/// Controlador principal que gere as p�ginas iniciais e institucionais.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Construtor que injeta o logger do controlador.
    /// </summary>
    /// <param name="logger">Inst�ncia de ILogger.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// P�gina inicial da aplica��o.
    /// </summary>
    /// <returns>Vista da p�gina inicial.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// P�gina de pol�tica de privacidade.
    /// </summary>
    /// <returns>Vista com informa��es de privacidade.</returns>
    public IActionResult Privacy()
    {
        return View();
    }
    /// <summary>
    /// P�gina "Sobre" com informa��es institucionais ou da equipa.
    /// </summary>
    /// <returns>Vista com a sec��o "Sobre".</returns>
    public IActionResult Sobre() {
        return View();
    }

    /// <summary>
    /// P�gina de erro apresentada em caso de exce��es n�o tratadas.
    /// </summary>
    /// <returns>Vista com detalhes do erro.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
