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
    public class AutodromoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutodromoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Autodromo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autodromos.ToListAsync());
        }

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

        // GET: Autodromo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autodromo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Autodromo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool AutodromoExists(int id)
        {
            return _context.Autodromos.Any(e => e.Id == id);
        }
    }
}
