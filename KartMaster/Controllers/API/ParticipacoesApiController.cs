using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace KartMaster.Controllers.API
{
    /// <summary>
    /// Controlador de API para fazer a gestão das participações em corridas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipacoesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipacoesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as participações em corridas, com informação sobre o utilizador e corrida.
        /// </summary>
        /// <returns>Lista de participações.</returns>
        /// <remarks>Requer autenticação JWT.</remarks>
        /// <response code="200">Lista devolvida com sucesso</response>
        /// <response code="401">Não autorizado</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ParticipacaoViewModel>), StatusCodes.Status200OK)]
        [Produces("application/json")]
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

            return Ok(viewModels);
        }

        /// <summary>
        /// Obtém uma participação específica por ID de corrida e utilizador.
        /// </summary>
        /// <param name="corridaId">ID da corrida.</param>
        /// <param name="utilizadorId">ID do utilizador.</param>
        /// <returns>Participação detalhada ou NotFound.</returns>
        /// <remarks>Requer autenticação JWT.</remarks>
        // GET: api/ParticipacoesApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        /// <summary>
        /// Atualiza os dados de uma participação existente.
        /// </summary>
        /// <param name="corridaId">ID da corrida.</param>
        /// <param name="utilizadorId">ID do utilizador.</param>
        /// <param name="dto">Dados atualizados da participação.</param>
        /// <returns>NoContent se bem-sucedido, NotFound ou BadRequest caso contrário.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // PUT: api/ParticipacoesApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Cria uma nova participação numa corrida.
        /// </summary>
        /// <param name="dto">Dados da nova participação.</param>
        /// <returns>CreatedAtAction com a nova participação.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // POST: api/ParticipacoesApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Elimina uma participação com base na corrida e no utilizador.
        /// </summary>
        /// <param name="corridaId">ID da corrida.</param>
        /// <param name="utilizadorId">ID do utilizador.</param>
        /// <returns>NoContent se removido, NotFound caso não exista.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // DELETE: api/ParticipacoesApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Verifica se uma participação com os IDs especificados existe.
        /// </summary>
        /// <param name="corridaId">ID da corrida.</param>
        /// <param name="utilizadorId">ID do utilizador.</param>
        /// <returns>True se existir, caso contrário false.</returns>
        private bool ParticipacaoExists(int corridaId, int utilizadorId) {
            return _context.Participacoes.Any(p => p.CorridaId == corridaId && p.UtilizadorId == utilizadorId);
        }
    }
}
