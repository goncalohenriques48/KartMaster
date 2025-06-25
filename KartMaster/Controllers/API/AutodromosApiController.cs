using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;

namespace KartMaster.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutodromosApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutodromosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AutodromosApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutodromoViewModel>>> GetAutodromos() {
            return await _context.Autodromos
                .Select(a => new AutodromoViewModel {
                    Nome = a.Nome,
                    Localizacao = a.Localizacao,
                    Telemovel = a.Telemovel,
                    Email = a.Email,
                    Capacidade = a.Capacidade
                })
                .ToListAsync();
        }

        // GET: api/AutodromosApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutodromoViewModel>> GetAutodromo(int id) {
            var autodromo = await _context.Autodromos.FindAsync(id);

            if (autodromo == null)
                return NotFound();

            return new AutodromoViewModel {
                Nome = autodromo.Nome,
                Localizacao = autodromo.Localizacao,
                Telemovel = autodromo.Telemovel,
                Email = autodromo.Email,
                Capacidade = autodromo.Capacidade
            };
        }

        // PUT: api/AutodromosApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutodromo(int id, AutodromoDto dto) {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null) return NotFound();

            autodromo.Nome = dto.Nome;
            autodromo.Localizacao = dto.Localizacao;
            autodromo.Telemovel = dto.Telemovel;
            autodromo.Email = dto.Email;
            autodromo.Capacidade = dto.Capacidade;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/AutodromosApi
        [HttpPost]
        public async Task<ActionResult> PostAutodromo(AutodromoDto dto) {
            var autodromo = new Autodromo {
                Nome = dto.Nome,
                Localizacao = dto.Localizacao,
                Telemovel = dto.Telemovel,
                Email = dto.Email,
                Capacidade = dto.Capacidade
            };

            _context.Autodromos.Add(autodromo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutodromo), new { id = autodromo.Id }, null);
        }

        // DELETE: api/AutodromosApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutodromo(int id) {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null)
                return NotFound();

            _context.Autodromos.Remove(autodromo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutodromoExists(int id)
        {
            return _context.Autodromos.Any(e => e.Id == id);
        }
    }
}
