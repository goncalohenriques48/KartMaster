using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;

namespace KartMaster.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão das reservas de utilizadores.
    /// Permite listar, criar, editar, visualizar e eliminar reservas.
    /// </summary>
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto da base de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação.</param>
        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra todas as reservas efetuadas pelo utilizador autenticado.
        /// </summary>
        /// <returns>Vista com a lista de reservas ou Unauthorized se o utilizador não estiver autenticado.</returns>
        [HttpGet]
        public async Task<IActionResult> MinhasReservas() {
            // Verificação segura do utilizador autenticado
            var username = User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            // Procurar o utilizador autenticado
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                return Unauthorized();

            // Obter reservas associadas ao utilizador
            var minhasReservas = await _context.Reservas
                .Include(r => r.Autodromo)
                .Include(r => r.Corrida)
                .Where(r => r.UtilizadorId == user.Id)
                .ToListAsync();

            return View(minhasReservas);
        }


        /// <summary>
        /// Mostra todas as reservas registadas no sistema.
        /// </summary>
        /// <returns>Vista com a lista completa de reservas.</returns>
        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservas.Include(r => r.Autodromo).Include(r => r.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de uma reserva específica.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Vista de detalhes ou erro 404 se não encontrada.</returns>
        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Autodromo)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        /// <summary>
        /// Apresenta o formulário para criação de uma nova reserva.
        /// </summary>
        /// <returns>Vista com o formulário de criação.</returns>
        // GET: Reserva/Create
        public IActionResult Create()
        {
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome");
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Submete uma nova reserva ao sistema.
        /// </summary>
        /// <param name="reserva">Objeto reserva a criar.</param>
        /// <returns>Redireciona para o Index ou retorna à vista de criação com erros.</returns>
        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeReservante,NumeroPessoas,Data,Hora,AutodromoId,UtilizadorId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Email", reserva.AutodromoId);
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", reserva.UtilizadorId);
            return View(reserva);
        }

        /// <summary>
        /// Apresenta o formulário para editar uma reserva existente.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Vista de edição ou erro 404 se não encontrada.</returns>
        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome", reserva.AutodromoId);
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", reserva.UtilizadorId);
            return View(reserva);
        }

        /// <summary>
        /// Submete as alterações de uma reserva.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <param name="reserva">Objeto reserva atualizado.</param>
        /// <returns>Redireciona para o Index ou vista de edição com erros.</returns>
        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeReservante,NumeroPessoas,Data,Hora,Duracao,AutodromoId,UtilizadorId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome", reserva.AutodromoId);
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", reserva.UtilizadorId);
            return View(reserva);
        }

        /// <summary>
        /// Apresenta a vista de confirmação de eliminação de uma reserva.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Vista de confirmação ou erro 404 se não encontrada.</returns>
        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Autodromo)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        /// <summary>
        /// Confirma e executa a remoção de uma reserva do sistema.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Redireciona para a lista de reservas.</returns>
        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe uma reserva com determinado ID.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>True se existir, false caso contrário.</returns>
        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
