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
    /// Controlador responsável pela gestão dos autódromos.
    /// </summary>
    public class AutodromoController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do controlador de autódromos.
        /// </summary>
        /// <param name="context">Contexto da base de dados.</param>
        public AutodromoController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os autódromos disponíveis.
        /// </summary>
        /// <returns>Vista com a lista de autódromos.</returns>
        // GET: Autodromo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autodromos.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um autódromo específico.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>Vista com os detalhes ou erro 404 se não encontrado.</returns>
        // GET: Autodromo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autodromo = await _context.Autodromos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autodromo == null)
            {
                return NotFound();
            }

            return View(autodromo);
        }

        /// <summary>
        /// Apresenta o formulário para criação de um novo autódromo.
        /// </summary>
        /// <returns>Vista de criação.</returns>
        // GET: Autodromo/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Regista um novo autódromo na base de dados.
        /// </summary>
        /// <param name="autodromo">Objeto com os dados do autódromo.</param>
        /// <returns>Redireciona para o índice ou vista com erros.</returns>
        // POST: Autodromo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Localizacao,Telemovel,Email,Capacidade")] Autodromo autodromo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autodromo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autodromo);
        }

        /// <summary>
        /// Apresenta o formulário para edição de um autódromo existente.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>Vista de edição ou erro 404 se não encontrado.</returns>
        // GET: Autodromo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null)
            {
                return NotFound();
            }
            return View(autodromo);
        }

        /// <summary>
        /// Atualiza os dados de um autódromo existente.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <param name="autodromo">Objeto com os dados atualizados.</param>
        /// <returns>Redireciona para o índice ou vista com erros.</returns>
        // POST: Autodromo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Localizacao,Telemovel,Email,Capacidade")] Autodromo autodromo)
        {
            if (id != autodromo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autodromo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutodromoExists(autodromo.Id))
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
            return View(autodromo);
        }

        /// <summary>
        /// Apresenta a confirmação de remoção de um autódromo.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>Vista de confirmação.</returns>
        // GET: Autodromo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autodromo = await _context.Autodromos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autodromo == null)
            {
                return NotFound();
            }

            return View(autodromo);
        }

        /// <summary>
        /// Remove um autódromo da base de dados.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>Redireciona para o índice após remoção.</returns>
        // POST: Autodromo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo != null)
            {
                _context.Autodromos.Remove(autodromo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se um autódromo com o ID especificado existe.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>True se existir, False caso contrário.</returns>
        private bool AutodromoExists(int id)
        {
            return _context.Autodromos.Any(e => e.Id == id);
        }
    }
}
