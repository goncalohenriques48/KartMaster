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
    public class CorridaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorridaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Disponiveis()
        {
            var corridas = await _context.Corridas
                .Include(c => c.Autodromo) // <- Isto é essencial
                .Where(c => c.Data > DateTime.Now)
                .ToListAsync();

            return View(corridas);
        }


        // GET: Corrida
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Corridas.Include(c => c.Autodromo);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: Corrida/Create
        public IActionResult Create()
        {
            ViewData["AutodromoId"] = new SelectList(_context.Autodromos, "Id", "Nome");
            return View();
        }

        // POST: Corrida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Corrida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool CorridaExists(int id)
        {
            return _context.Corridas.Any(e => e.Id == id);
        }
    }
}
