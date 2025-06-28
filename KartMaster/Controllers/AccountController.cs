using Microsoft.AspNetCore.Mvc;
using KartMaster.Models;

namespace KartMaster.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação e registo de utilizadores.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Mostra a página de login.
        /// </summary>
        /// <returns>Vista de login.</returns>
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Processa o pedido de login submetido pelo utilizador.
        /// <remarks>Atualmente esta ação redireciona diretamente para a página inicial sem validação.</remarks>
        /// </summary>
        /// <param name="model">Modelo de utilizador com os dados introduzidos.</param>
        /// <returns>Redireciona para a página inicial.</returns>
        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(Utilizador model)
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Mostra a página de registo de novo utilizador.
        /// </summary>
        /// <returns>Vista de registo.</returns>
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Processa o registo de um novo utilizador.
        /// <remarks>Atualmente esta ação redireciona para a página de login sem guardar dados.</remarks>
        /// </summary>
        /// <param name="model">Modelo de utilizador com os dados submetidos.</param>
        /// <returns>Redireciona para a página de login.</returns>
        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(Utilizador model)
        {
            // Lógica de registo deverá ser implementada aqui
            return RedirectToAction("Login");
        }
    }
}
