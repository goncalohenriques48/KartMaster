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
    /// Controlador responsável pela gestão dos Utilizadores da aplicação.
    /// Permite criar, editar, visualizar e apagar utilizadores.
    /// </summary>
    public class UtilizadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do controlador que recebe o contexto da base de dados.
        /// </summary>
        /// <param name="context">Contexto da base de dados.</param>
        public UtilizadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os utilizadores.
        /// </summary>
        /// <returns>Vista com a lista de utilizadores.</returns>
        // GET: Utilizador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizadores.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um utilizador específico.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <returns>Vista com os detalhes do utilizador ou erro 404 se não existir.</returns>
        // GET: Utilizador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        /// <summary>
        /// Apresenta o formulário para criar um novo utilizador.
        /// </summary>
        /// <returns>Vista do formulário de criação.</returns>
        // GET: Utilizador/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Submete os dados do novo utilizador.
        /// </summary>
        /// <param name="utilizador">Objeto Utilizador a ser criado.</param>
        /// <returns>Redireciona para a lista ou volta à vista de criação com erros.</returns>
        // POST: Utilizador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        /// <summary>
        /// Apresenta o formulário para editar um utilizador.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <returns>Vista de edição ou erro 404 se não encontrado.</returns>
        // GET: Utilizador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        /// <summary>
        /// Submete as alterações de um utilizador.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <param name="utilizador">Objeto atualizado.</param>
        /// <returns>Redireciona para a lista ou vista de edição com erros.</returns>
        // POST: Utilizador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Utilizador utilizador)
        {
            if (id != utilizador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.Id))
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
            return View(utilizador);
        }

        /// <summary>
        /// Apresenta a vista de confirmação de eliminação de um utilizador.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <returns>Vista de confirmação ou erro 404 se não encontrado.</returns>
        // GET: Utilizador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        /// <summary>
        /// Confirma e executa a eliminação de um utilizador.
        /// </summary>
        /// <param name="id">ID do utilizador a eliminar.</param>
        /// <returns>Redireciona para a lista de utilizadores.</returns>
        // POST: Utilizador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                _context.Utilizadores.Remove(utilizador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se um utilizador com determinado ID existe.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <returns>True se existir, false caso contrário.</returns>
        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }
    }
}
