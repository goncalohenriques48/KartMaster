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
    public class ParticipacoesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipacoesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ParticipacoesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipacaoViewModel>>> GetParticipacoes() {
            var participacoes = await _context.Participacoes
                .Include(p => p.Corrida)
                .Include(p => p.Utilizador)
                .ToListAsync();

            var viewModels = participacoes.Select(p => new ParticipacaoViewModel {
                NomeCorrida = p.Corrida?.Nome ?? "Corrida desconhecida",
                NomeUtilizador = p.Utilizador?.Nome ?? "Utilizador desconhecido",
                PosicaoFinal = p.PosicaoFinal,
                TempoFinal = p.TempoFinal.ToString()
            }).ToList();

            return viewModels;
        }

        // GET: api/ParticipacoesApi/5
        [HttpGet("{corridaId}/{utilizadorId}")]
        public async Task<ActionResult<ParticipacaoViewModel>> GetParticipacao(int corridaId, int utilizadorId) {
            var participacao = await _context.Participacoes
                .Include(p => p.Corrida)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(p => p.CorridaId == corridaId && p.UtilizadorId == utilizadorId);

            if (participacao == null)
                return NotFound();

            var viewModel = new ParticipacaoViewModel {
                NomeCorrida = participacao.Corrida?.Nome,
                NomeUtilizador = participacao.Utilizador?.Nome,
                PosicaoFinal = participacao.PosicaoFinal,
                TempoFinal = participacao.TempoFinal.ToString(@"hh\:mm\:ss")
            };

            return viewModel;
        }

        // PUT: api/ParticipacoesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{corridaId}/{utilizadorId}")]
        public async Task<IActionResult> PutParticipacao(int corridaId, int utilizadorId, ParticipacaoDto dto) {
            if (corridaId != dto.CorridaId || utilizadorId != dto.UtilizadorId)
                return BadRequest();

            var participacao = await _context.Participacoes
                .FirstOrDefaultAsync(p => p.CorridaId == corridaId && p.UtilizadorId == utilizadorId);

            if (participacao == null)
                return NotFound();

            participacao.PosicaoFinal = dto.PosicaoFinal;
            participacao.TempoFinal = dto.TempoFinal;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ParticipacoesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostParticipacao(ParticipacaoDto dto) {
            var participacao = new Participacao {
                CorridaId = dto.CorridaId,
                UtilizadorId = dto.UtilizadorId,
                PosicaoFinal = dto.PosicaoFinal,
                TempoFinal = dto.TempoFinal
            };

            _context.Participacoes.Add(participacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParticipacao), new { corridaId = dto.CorridaId, utilizadorId = dto.UtilizadorId }, null);
        }

        // DELETE: api/ParticipacoesApi/5
        [HttpDelete("{corridaId}/{utilizadorId}")]
        public async Task<IActionResult> DeleteParticipacao(int corridaId, int utilizadorId) {
            var participacao = await _context.Participacoes
                .FirstOrDefaultAsync(p => p.CorridaId == corridaId && p.UtilizadorId == utilizadorId);

            if (participacao == null)
                return NotFound();

            _context.Participacoes.Remove(participacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipacaoExists(int corridaId, int utilizadorId) {
            return _context.Participacoes.Any(p => p.CorridaId == corridaId && p.UtilizadorId == utilizadorId);
        }
    }
}
