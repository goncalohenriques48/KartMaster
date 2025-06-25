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
    public class UtilizadoresApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilizadoresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UtilizadoresApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilizadorViewModel>>> GetUtilizadores() {
            var utilizadores = await _context.Utilizadores
                .Select(u => new UtilizadorViewModel {
                    Nome = u.Nome,
                    Email = u.Email,
                    UserName = u.UserName
                })
                .ToListAsync();

            return utilizadores;
        }

        // GET: api/UtilizadoresApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilizadorViewModel>> GetUtilizador(int id) {
            var utilizador = await _context.Utilizadores.FindAsync(id);

            if (utilizador == null)
                return NotFound();

            var viewModel = new UtilizadorViewModel {
                Nome = utilizador.Nome,
                Email = utilizador.Email,
                UserName = utilizador.UserName
            };

            return viewModel;
        }

        // DELETE: api/UtilizadoresApi/5
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
