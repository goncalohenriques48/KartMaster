using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;

namespace KartMaster.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public class CorridasApiController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public CorridasApiController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: api/CorridasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorridaViewModel>>> GetCorridas() {
            var corridas = await _context.Corridas
                .Include(c => c.Autodromo)
                .Include(c => c.Participacoes)
                .Select(c => new CorridaViewModel {
                    Nome = c.Nome,
                    Data = c.Data,
                    Hora = c.Hora,
                    Duracao = c.Duracao,
                    NomeAutodromo = c.Autodromo.Nome,
                    NumeroParticipantes = c.Participacoes.Count
                })
                .ToListAsync();

            return Ok(corridas);
        }

        // GET: api/CorridasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CorridaViewModel>> GetCorrida(int id) {
            var corrida = await _context.Corridas
                .Include(c => c.Autodromo)
                .Include(c => c.Participacoes)
                .Where(c => c.Id == id)
                .Select(c => new CorridaViewModel {
                    Nome = c.Nome,
                    Data = c.Data,
                    Hora = c.Hora,
                    Duracao = c.Duracao,
                    NomeAutodromo = c.Autodromo.Nome,
                    NumeroParticipantes = c.Participacoes.Count
                })
                .FirstOrDefaultAsync();

            if (corrida == null) {
                return NotFound();
            }

            return Ok(corrida);
        }

        // POST: api/CorridasApi
        [HttpPost]
        public async Task<ActionResult<Corrida>> PostCorrida(CorridaDto dto) {
            var corrida = new Corrida {
                Nome = dto.Nome,
                Data = dto.Data,
                Hora = dto.Hora,
                Duracao = dto.Duracao,
                AutodromoId = dto.AutodromoId
            };

            _context.Corridas.Add(corrida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCorrida), new { id = corrida.Id }, corrida);
        }

        // PUT: api/CorridasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorrida(int id, CorridaDto dto) {
            var corrida = await _context.Corridas.FindAsync(id);
            if (corrida == null)
                return NotFound();

            corrida.Nome = dto.Nome;
            corrida.Data = dto.Data;
            corrida.Hora = dto.Hora;
            corrida.Duracao = dto.Duracao;
            corrida.AutodromoId = dto.AutodromoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/CorridasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorrida(int id) {
            var corrida = await _context.Corridas.FindAsync(id);
            if (corrida == null) {
                return NotFound();
            }

            _context.Corridas.Remove(corrida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorridaExists(int id) {
            return _context.Corridas.Any(e => e.Id == id);
        }
    }
}
