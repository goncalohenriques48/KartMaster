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
        /// <summary>
        /// Página para escolher a data de uma corrida, filtrando por autódromo e excluindo corridas já reservadas pelo utilizador.
        /// </summary>
        /// <param name="id">ID do autódromo.</param>
        /// <returns>View com autódromo, corridas disponíveis e corridas já reservadas.</returns>
        public async Task<IActionResult> EscolherData(int id) {
            // Procurar o autódromo
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null) return NotFound();

            // Procurar corridas futuras no autódromo
            var corridas = await _context.Corridas
                .Where(c => c.AutodromoId == id && c.Data >= DateTime.Today)
                .ToListAsync();

            // Verificação segura do utilizador autenticado
            var username = User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            // Obter o ID do utilizador
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return Unauthorized();

            // Corridas já reservadas pelo utilizador
            var corridasReservadas = await _context.Reservas
                .Where(r => r.UtilizadorId == user.Id)
                .Select(r => r.CorridaId)
                .ToListAsync();

            // Passar dados para a View
            ViewBag.Autodromo = autodromo;
            ViewBag.Corridas = corridas;
            ViewBag.CorridasReservadas = corridasReservadas;

            return View();
        }

        // POST: /Agendamento/ConfirmarReserva
        /// <summary>
        /// Confirma uma reserva para a corrida especificada, evitando duplicações.
        /// </summary>
        /// <param name="AutodromoId">ID do autódromo onde decorre a corrida.</param>
        /// <param name="NomeReservante">Nome da pessoa que reserva.</param>
        /// <param name="NumeroPessoas">Número de participantes.</param>
        /// <param name="CorridaId">ID da corrida a reservar.</param>
        /// <returns>Redireciona para página de sucesso ou erro.</returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmarReserva(int AutodromoId, string NomeReservante, int NumeroPessoas, int CorridaId) {
            // Verificação segura do utilizador autenticado
            var username = User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                return Unauthorized();

            var corrida = await _context.Corridas.FindAsync(CorridaId);
            if (corrida == null)
                return NotFound("Corrida não encontrada.");

            // Verificar se já existe uma reserva para esta corrida
            bool jaReservada = await _context.Reservas
                .AnyAsync(r => r.CorridaId == CorridaId && r.UtilizadorId == user.Id);

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
                CorridaId = CorridaId,
                UtilizadorId = user.Id
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Reserva efetuada com sucesso!";
            return RedirectToAction("Index", "Agendamento");
        }


    }
}
