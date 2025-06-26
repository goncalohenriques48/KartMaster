using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using KartMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace KartMaster.Controllers.API {
    /// <summary>
    /// API para operações relacionadas com corridas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CorridasApiController : ControllerBase {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do controlador CorridasApiController.
        /// </summary>
        /// <param name="context">Contexto da base de dados.</param>
        public CorridasApiController(ApplicationDbContext context) {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de todas as corridas com detalhes básicos.
        /// </summary>
        /// <remarks>Este endpoint é público e não requer autenticação.</remarks>
        /// <returns>Lista de objetos <see cref="CorridaViewModel"/>.</returns>
        // GET: api/CorridasApi
        [AllowAnonymous]
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

        /// <summary>
        /// Obtém os detalhes de uma corrida específica por ID.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <returns>Objeto <see cref="CorridaViewModel"/> se encontrado; caso contrário, NotFound.</returns>
        // GET: api/CorridasApi/5
        [AllowAnonymous]
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

        /// <summary>
        /// Adiciona uma nova corrida à base de dados.
        /// </summary>
        /// <param name="dto">Objeto <see cref="CorridaDto"/> com os dados da nova corrida.</param>
        /// <returns>Objeto <see cref="Corrida"/> criado com código 201; caso contrário, erro.</returns>
        /// <remarks>Requer autenticação como administrador via JWT.</remarks>
        // POST: api/CorridasApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Atualiza os dados de uma corrida existente.
        /// </summary>
        /// <param name="id">ID da corrida a atualizar.</param>
        /// <param name="dto">Objeto <see cref="CorridaDto"/> com os dados atualizados.</param>
        /// <returns>Resposta HTTP 204 NoContent se atualizado com sucesso; NotFound se não existir.</returns>
        /// <remarks>Requer autenticação como administrador via JWT.</remarks>
        // PUT: api/CorridasApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Remove uma corrida da base de dados.
        /// </summary>
        /// <param name="id">ID da corrida a remover.</param>
        /// <returns>Resposta HTTP 204 NoContent se removido; NotFound se não existir.</returns>
        /// <remarks>Requer autenticação como administrador via JWT.</remarks>
        // DELETE: api/CorridasApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Verifica se uma corrida com o ID especificado existe na base de dados.
        /// </summary>
        /// <param name="id">ID da corrida.</param>
        /// <returns>True se existir; caso contrário, false.</returns>
        private bool CorridaExists(int id) {
            return _context.Corridas.Any(e => e.Id == id);
        }
    }
}
