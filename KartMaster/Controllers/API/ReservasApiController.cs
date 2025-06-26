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
    /// Controlador de API responsável pela gestão de reservas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as reservas com informação sobre corrida, autódromo e utilizador.
        /// </summary>
        /// <returns>Lista de objetos ReservaViewModel.</returns>
        /// <remarks>Requer autenticação JWT.</remarks>
        // GET: api/ReservasApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaViewModel>>> GetReservas() {
            var reservas = await _context.Reservas
                .Include(r => r.Autodromo)
                .Include(r => r.Corrida)
                .Include(r => r.Utilizador)
                .Select(r => new ReservaViewModel {
                    NomeReservante = r.NomeReservante,
                    NumeroPessoas = r.NumeroPessoas,
                    Data = r.Data.ToString("dd-MM-yyyy"),
                    Hora = r.Hora.ToString(@"hh\:mm"),
                    Duracao = r.Duracao.ToString(@"hh\:mm\:ss"),
                    NomeAutodromo = r.Autodromo.Nome,
                    NomeCorrida = r.Corrida.Nome
                })
                .ToListAsync();

            return reservas;
        }

        /// <summary>
        /// Obtém uma reserva específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Objeto ReservaViewModel correspondente ou NotFound.</returns>
        /// <remarks>Requer autenticação JWT.</remarks>
        // GET: api/ReservasApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaViewModel>> GetReserva(int id) {
            var reserva = await _context.Reservas
                .Include(r => r.Autodromo)
                .Include(r => r.Corrida)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
                return NotFound();

            var viewModel = new ReservaViewModel {
                NomeReservante = reserva.NomeReservante,
                NumeroPessoas = reserva.NumeroPessoas,
                Data = reserva.Data.ToString("dd-MM-yyyy"),
                Hora = reserva.Hora.ToString(@"hh\:mm"),
                Duracao = reserva.Duracao.ToString(@"hh\:mm\:ss"),
                NomeAutodromo = reserva.Autodromo.Nome,
                NomeCorrida = reserva.Corrida.Nome
            };

            return viewModel;
        }

        /// <summary>
        /// Atualiza uma reserva existente com base no ID.
        /// </summary>
        /// <param name="id">ID da reserva a atualizar.</param>
        /// <param name="dto">Dados atualizados da reserva.</param>
        /// <returns>NoContent se a atualização for bem-sucedida, NotFound se a reserva não existir.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // PUT: api/ReservasApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, ReservaDto dto) {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
                return NotFound();

            reserva.NomeReservante = dto.NomeReservante;
            reserva.NumeroPessoas = dto.NumeroPessoas;
            reserva.Data = dto.Data;
            reserva.Hora = dto.Hora;
            reserva.Duracao = dto.Duracao;
            reserva.AutodromoId = dto.AutodromoId;
            reserva.UtilizadorId = dto.UtilizadorId;
            reserva.CorridaId = dto.CorridaId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cria uma nova reserva.
        /// </summary>
        /// <param name="dto">Dados da nova reserva.</param>
        /// <returns>Resposta CreatedAtAction com a nova reserva criada.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // POST: api/ReservasApi
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PostReserva(ReservaDto dto) {
            var reserva = new Reserva {
                NomeReservante = dto.NomeReservante,
                NumeroPessoas = dto.NumeroPessoas,
                Data = dto.Data,
                Hora = dto.Hora,
                Duracao = dto.Duracao,
                AutodromoId = dto.AutodromoId,
                UtilizadorId = dto.UtilizadorId,
                CorridaId = dto.CorridaId
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, null);
        }

        /// <summary>
        /// Elimina uma reserva com base no ID.
        /// </summary>
        /// <param name="id">ID da reserva a eliminar.</param>
        /// <returns>NoContent se a remoção for bem-sucedida, NotFound se a reserva não existir.</returns>
        /// <remarks>Requer autenticação JWT com permissões de administrador.</remarks>
        // DELETE: api/ReservasApi/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id) {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
                return NotFound();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se uma reserva com o ID especificado existe na base de dados.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>True se a reserva existir, false caso contrário.</returns>
        private bool ReservaExists(int id) {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
