using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;
using Microsoft.AspNetCore.Authorization;


namespace KartMaster.Controllers
{
    public class ParticipacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Apresenta a lista de participações do utilizador autenticado.
        /// </summary>
        /// <returns>View com a lista de participações ou erro 401 se não autenticado.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MinhasParticipacoes()
        {
            // Verifica se o utilizador está autenticado
            var username = User?.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            // Obtém o IdentityUser associado ao username
            var identityUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (identityUser == null)
                return Unauthorized();

            // Obtém o utilizador personalizado associado ao IdentityUser
            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

            if (utilizador == null)
                return Unauthorized();

            // Procura as participações do utilizador, incluindo os dados da corrida
            var participacoes = await _context.Participacoes
                .Include(p => p.Corrida)
                .Where(p => p.UtilizadorId == utilizador.Id)
                .ToListAsync();

            return View(participacoes);
        }




        // GET: Participacao
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Participacoes.Include(p => p.Corrida).Include(p => p.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Participacao/Details/5
        public async Task<IActionResult> Details(int? id, int? corridaId) {
            if (id == null || corridaId == null) {
                return NotFound();
            }

            var participacao = await _context.Participacoes
                .Include(p => p.Corrida)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.UtilizadorId == id && m.CorridaId == corridaId);
            if (participacao == null) {
                return NotFound();
            }

            return View(participacao);
        }

        // GET: Participacao/Create
        public IActionResult Create()
        {
            ViewData["CorridaId"] = new SelectList(_context.Corridas, "Id", "Nome");
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "Id", "Nome");
            return View();
        }

        // POST: Participacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilizadorId,CorridaId,PosicaoFinal,TempoFinal")] Participacao participacao)
        {
            // Verificar se já existe uma participação com a mesma chave composta
            var exists = await _context.Participacoes
                .AnyAsync(p => p.UtilizadorId == participacao.UtilizadorId && p.CorridaId == participacao.CorridaId);

            if (exists) {
                ModelState.AddModelError(string.Empty, "Já existe uma participação para este utilizador nesta corrida.");
            }

            if (!ModelState.IsValid) {
                // Recarregar os dados para os dropdowns
                ViewData["CorridaId"] = new SelectList(_context.Corridas, "Id", "Nome", participacao.CorridaId);
                ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "Id", "Nome", participacao.UtilizadorId);
                return View(participacao);
            }

            // Se o registro não existir, salva a participação
            _context.Add(participacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Participacao/Edit/5
        public async Task<IActionResult> Edit(int? id, int? corridaId) {
            if (id == null || corridaId == null) {
                return NotFound();
            }

            var participacao = await _context.Participacoes
                .FirstOrDefaultAsync(p => p.UtilizadorId == id && p.CorridaId == corridaId);
            if (participacao == null) {
                return NotFound();
            }

            ViewData["CorridaId"] = new SelectList(_context.Corridas, "Id", "Nome", participacao.CorridaId);
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "Id", "Nome", participacao.UtilizadorId);
            return View(participacao);
        }

        // POST: Participacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int corridaId, [Bind("UtilizadorId,CorridaId,PosicaoFinal,TempoFinal")] Participacao participacao) {
            if (id != participacao.UtilizadorId || corridaId != participacao.CorridaId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(participacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ParticipacaoExists(participacao.UtilizadorId, participacao.CorridaId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CorridaId"] = new SelectList(_context.Corridas, "Id", "Nome", participacao.CorridaId);
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "Id", "Nome", participacao.UtilizadorId);
            return View(participacao);
        }

        private bool ParticipacaoExists(int id, int corridaId) {
            return _context.Participacoes.Any(e => e.UtilizadorId == id && e.CorridaId == corridaId);
        }

        // GET: Participacao/Delete/5
        public async Task<IActionResult> Delete(int? id, int? corridaId) {
            if (id == null || corridaId == null) {
                return NotFound();
            }

            var participacao = await _context.Participacoes
                .Include(p => p.Corrida)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.UtilizadorId == id && m.CorridaId == corridaId);
            if (participacao == null) {
                return NotFound();
            }

            return View(participacao);
        }

        // POST: Participacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int corridaId) {
            var participacao = await _context.Participacoes
                .FirstOrDefaultAsync(p => p.UtilizadorId == id && p.CorridaId == corridaId);
            if (participacao != null) {
                _context.Participacoes.Remove(participacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacaoExists(int id)
        {
            return _context.Participacoes.Any(e => e.UtilizadorId == id);
        }
    }
}
