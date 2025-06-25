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
    public class ReservasApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservasApi
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

        // GET: api/ReservasApi/5
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

        // PUT: api/ReservasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/ReservasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/ReservasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id) {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
                return NotFound();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id) {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
