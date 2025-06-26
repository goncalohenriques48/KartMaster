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
    /// Controlador de API responsável pela gestão de utilizadores registados.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadoresApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilizadoresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de todos os utilizadores registados.
        /// </summary>
        /// <returns>Lista de objetos UtilizadorViewModel.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // GET: api/UtilizadoresApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilizadorViewModel>>> GetUtilizadores() {
            var utilizadores = await _context.Utilizadores
                .Select(u => new UtilizadorViewModel {
                    Nome = u.Nome,
                    Email = u.Email,
                })
                .ToListAsync();

            return utilizadores;
        }

        /// <summary>
        /// Obtém os dados de um utilizador específico pelo ID.
        /// </summary>
        /// <param name="id">ID do utilizador.</param>
        /// <returns>Objeto UtilizadorViewModel correspondente ou NotFound.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // GET: api/UtilizadoresApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilizadorViewModel>> GetUtilizador(int id) {
            var utilizador = await _context.Utilizadores.FindAsync(id);

            if (utilizador == null)
                return NotFound();

            var viewModel = new UtilizadorViewModel {
                Nome = utilizador.Nome,
                Email = utilizador.Email,
            };

            return viewModel;
        }

        /// <summary>
        /// Elimina um utilizador pelo ID.
        /// </summary>
        /// <param name="id">ID do utilizador a eliminar.</param>
        /// <returns>NoContent se a eliminação for bem-sucedida, NotFound se o utilizador não existir.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // DELETE: api/UtilizadoresApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizador(int id) {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
                return NotFound();

            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
