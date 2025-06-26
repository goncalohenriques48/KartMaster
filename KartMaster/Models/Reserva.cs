using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KartMaster.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? NomeReservante { get; set; }

        [Range(1, 20, ErrorMessage = "Indique entre 1 e 20 pessoas.")]
        public int NumeroPessoas { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public TimeSpan Duracao { get; set; }

        // Autódromo relacionado
        public int AutodromoId { get; set; }
        public Autodromo Autodromo { get; set; } = null!;

        // Utilizador autenticado
        public string? UtilizadorId { get; set; }
        public IdentityUser Utilizador { get; set; } = null!;

        // Corrida relacionada
        [Required]
        public int CorridaId { get; set; }
        public Corrida Corrida { get; set; } = null!;
    }
}

