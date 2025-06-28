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
    /// Controlador responsável pela gestão das corridas.
    /// </summary>
    public class CorridaController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto da base de dados.
        /// </summary>
        /// <param name="context">Contexto da base de dados.</param>
        public CorridaController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista as corridas futuras disponíveis.
        /// </summary>
        /// <returns>Vista com corridas cuja data é superior à atual.</returns>
        [HttpGet]
        public async Task<IActionResult> Disponiveis()
        {
            var corridas = await _context.Corridas
                .Include(c => c.Autodromo) // <- Isto é essencial
                .Where(c => c.Data > DateTime.Now)
                .ToListAsync();

            return View(corridas);
        }


        /// <summary>
        /// Lista todas as corridas disponíveis na base de dados.
        /// </summary>
        /// <returns>Vista com todas as corridas.</returns>
        // GET: Corrida
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Corridas.Include(c => c.Autodromo);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de uma corrida específica.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <returns>Vista com os detalhes da corrida.</returns>
        // GET: Corrida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corrida = await _context.Corridas
                .Include(c => c.Autodromo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corrida == null)
            {
                return NotFound();
            }

            return View(corrida);
        }

        /// <summary>
        /// Apresenta o formulário para criar uma nova corrida.
        /// </summary>
        /// <returns>Vista de criação de corrida.</returns>
        // GET: Corrida/Create
        public IActionResult Create()
        {
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome");
            return View();
        }

        /// <summary>
        /// Regista uma nova corrida na base de dados.
        /// </summary>
        /// <param name="Nome">Nome da corrida.</param>
        /// <param name="Data">Data da corrida.</param>
        /// <param name="HoraHoras">Hora (hora) da corrida.</param>
        /// <param name="HoraMinutos">Hora (minutos) da corrida.</param>
        /// <param name="DuracaoHoras">Duração (horas) da corrida.</param>
        /// <param name="DuracaoMinutos">Duração (minutos) da corrida.</param>
        /// <param name="DuracaoSegundos">Duração (segundos) da corrida.</param>
        /// <param name="AutodromoId">ID do autódromo associado.</param>
        /// <returns>Redireciona para o índice ou retorna a vista com erros.</returns>
        // POST: Corrida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Nome, DateTime Data, int HoraHoras, int HoraMinutos, int DuracaoHoras, int DuracaoMinutos, int DuracaoSegundos, int AutodromoId)
        {
            var hora = new TimeSpan(HoraHoras, HoraMinutos, 0);
            var duracao = new TimeSpan(DuracaoHoras, DuracaoMinutos, DuracaoSegundos);

            var corrida = new Corrida
            {
                Nome = Nome,
                Data = Data,
                Hora = hora,
                Duracao = duracao,
                AutodromoId = AutodromoId
            };

            if (ModelState.IsValid)
            {
                _context.Add(corrida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome", AutodromoId);
            return View(corrida);
        }


        /// <summary>
        /// Apresenta o formulário para editar uma corrida existente.
        /// </summary>
        /// <param name="id">ID da corrida a editar.</param>
        /// <returns>Vista de edição da corrida.</returns>
        // GET: Corrida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corrida = await _context.Corridas.FindAsync(id);
            if (corrida == null)
            {
                return NotFound();
            }
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome", corrida.AutodromoId);
            return View(corrida);
        }

        /// <summary>
        /// Atualiza os dados de uma corrida existente.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <param name="corrida">Objeto corrida com os novos dados.</param>
        /// <returns>Redireciona para o índice ou retorna a vista com erros.</returns>
        // POST: Corrida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Data,Hora,Duracao,AutodromoId")] Corrida corrida)

        {
            if (id != corrida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corrida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorridaExists(corrida.Id))
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
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome", corrida.AutodromoId);
            return View(corrida);
        }

        /// <summary>
        /// Apresenta a confirmação de remoção de uma corrida.
        /// </summary>
        /// <param name="id">ID da corrida a remover.</param>
        /// <returns>Vista de confirmação.</returns>
        // GET: Corrida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corrida = await _context.Corridas
                .Include(c => c.Autodromo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corrida == null)
            {
                return NotFound();
            }

            return View(corrida);
        }

        /// <summary>
        /// Remove uma corrida da base de dados.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <returns>Redireciona para o índice após remoção.</returns>
        // POST: Corrida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corrida = await _context.Corridas.FindAsync(id);
            if (corrida != null)
            {
                _context.Corridas.Remove(corrida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se uma corrida com determinado ID existe.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <returns>True se existir, False caso contrário.</returns>
        private bool CorridaExists(int id)
        {
            return _context.Corridas.Any(e => e.Id == id);
        }
    }
}
