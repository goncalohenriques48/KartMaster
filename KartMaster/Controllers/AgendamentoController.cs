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
        public async Task<IActionResult> EscolherData(int id)
        {
            var autodromo = await _context.Autodromos.FindAsync(id);
            if (autodromo == null) return NotFound();

            ViewBag.Autodromo = autodromo;
            return View();
        }

        // POST: /Agendamento/ConfirmarReserva
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ConfirmarReserva(int AutodromoId, string NomeReservante, int NumeroPessoas, DateTime Data, TimeSpan Hora)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            if (userId == null)
                return Unauthorized();

            var reserva = new Reserva
            {
                AutodromoId = AutodromoId,
                NomeReservante = NomeReservante,
                NumeroPessoas = NumeroPessoas,
                Data = Data,
                Hora = Hora,
                UtilizadorId = userId
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Reserva efetuada com sucesso!";
            return RedirectToAction("Index");
        }

    }
}
