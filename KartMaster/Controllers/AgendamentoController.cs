using Microsoft.AspNetCore.Mvc;
using KartMaster.Data;
using Microsoft.EntityFrameworkCore;
using KartMaster.Models;

namespace KartMaster.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Agendamento/
        public async Task<IActionResult> Index()
        {
            var autodromos = await _context.Autodromos.ToListAsync();
            return View(autodromos); // Vai mostrar lista de autódromos
        }

        // GET: /Agendamento/EscolherData/5
        public async Task<IActionResult> EscolherData(int id) {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null) return NotFound();

            var corridas = await _context.Corridas
                .Where(c => c.AutodromoId == id && c.Data >= DateTime.Today)
                .ToListAsync();

            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            var corridasReservadas = await _context.Reservas
                .Where(r => r.UtilizadorId == userId)
                .Select(r => r.CorridaId)
                .ToListAsync();

            ViewBag.Autodromo = autodromo;
            ViewBag.Corridas = corridas;
            ViewBag.CorridasReservadas = corridasReservadas;

            return View();
        }

        // POST: /Agendamento/ConfirmarReserva
        [HttpPost]
        public async Task<IActionResult> ConfirmarReserva(int AutodromoId, string NomeReservante, int NumeroPessoas, int CorridaId) {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            if (userId == null)
                return Unauthorized();

            var corrida = await _context.Corridas.FindAsync(CorridaId);
            if (corrida == null)
                return NotFound("Corrida não encontrada.");

            // ✅ Verificar se já existe uma reserva para esta corrida pelo mesmo utilizador
            bool jaReservada = await _context.Reservas
                .AnyAsync(r => r.CorridaId == CorridaId && r.UtilizadorId == userId);

            if (jaReservada) {
                TempData["Erro"] = "Já efetuaste uma reserva para esta corrida.";
                return RedirectToAction("EscolherData", new { id = AutodromoId });
            }

            var reserva = new Reserva {
                AutodromoId = AutodromoId,
                NomeReservante = NomeReservante,
                NumeroPessoas = NumeroPessoas,
                Data = corrida.Data,
                Hora = corrida.Hora,
                Duracao = corrida.Duracao,
                CorridaId = CorridaId, // 👈 agora também guardamos o ID da corrida
                UtilizadorId = userId
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Reserva efetuada com sucesso!";
            return RedirectToAction("Index", "Agendamento");
        }


    }
}
