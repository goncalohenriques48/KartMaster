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
    /// Controlador responsável pela gestão de autódromos via API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutodromosApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do controlador de autódromos.
        /// </summary>
        /// <param name="context">Contexto da base de dados.</param>
        public AutodromosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os autódromos disponíveis.
        /// </summary>
        /// <remarks>Este endpoint é público e não requer autenticação.</remarks>
        /// <returns>Lista de autódromos com os respetivos detalhes.</returns>
        // GET: api/AutodromosApi
        [AllowAnonymous]
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

        /// <summary>
        /// Obtém os detalhes de um autódromo específico.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <remarks>Este endpoint é público e não requer autenticação.</remarks>
        /// <returns>Objeto AutodromoViewModel ou NotFound.</returns>
        // GET: api/AutodromosApi/5
        [AllowAnonymous]
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

        /// <summary>
        /// Atualiza os dados de um autódromo existente.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <param name="dto">Objeto com os novos dados do autódromo.</param>
        /// <remarks>Requer autenticação com o perfil <b>Admin</b>.</remarks>
        /// <returns>NoContent se atualizado com sucesso, ou NotFound se o autódromo não existir.</returns>
        // PUT: api/AutodromosApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Cria um novo autódromo.
        /// </summary>
        /// <param name="dto">Objeto com os dados do autódromo a criar.</param>
        /// <remarks>Requer autenticação com o perfil <b>Admin</b>.</remarks>
        /// <returns>CreatedAtAction com os dados do autódromo criado.</returns>
        // POST: api/AutodromosApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        /// <summary>
        /// Elimina um autódromo existente com base no ID.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <remarks>Requer autenticação com o perfil <b>Admin</b>.</remarks>
        /// <returns>NoContent se eliminado, ou NotFound se não existir.</returns>
        // DELETE: api/AutodromosApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutodromo(int id) {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null)
                return NotFound();

            _context.Autodromos.Remove(autodromo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se um autódromo com o ID especificado existe na base de dados.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>True se existir, caso contrário false.</returns>
        private bool AutodromoExists(int id)
        {
            return _context.Autodromos.Any(e => e.Id == id);
        }
    }
}
