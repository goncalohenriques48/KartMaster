using Microsoft.AspNetCore.Mvc;
using KartMaster.Data;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult ConfirmarReserva(int autodromoId, DateTime data, TimeSpan hora)
        {
            // Simulação: no futuro pode guardar numa tabela 'Reserva' ou 'Participacao'
            TempData["Mensagem"] = $"Reserva feita com sucesso para o autódromo ID {autodromoId} no dia {data:dd/MM/yyyy} às {hora}.";
            return RedirectToAction("Index");
        }
    }
}
