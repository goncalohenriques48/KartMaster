using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa uma reserva feita por um utilizador num autódromo para participar numa corrida.
    /// </summary>
    public class Reserva
    {
        /// <summary>
        /// Identificador único da reserva.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da pessoa que efetuou a reserva.
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? NomeReservante { get; set; }

        /// <summary>
        /// Número de pessoas incluídas na reserva (mínimo 1, máximo 20).
        /// </summary>
        [Range(1, 20, ErrorMessage = "Indique entre 1 e 20 pessoas.")]
        public int NumeroPessoas { get; set; }

        /// <summary>
        /// Data da reserva.
        /// </summary>
        [Required]
        public DateTime Data { get; set; }

        /// <summary>
        /// Hora em que a reserva está marcada.
        /// </summary>
        [Required]
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Duração da reserva.
        /// </summary>
        [Required]
        public TimeSpan Duracao { get; set; }

        /// <summary>
        /// Chave estrangeira do autódromo onde a reserva será realizada.
        /// </summary>
        public int AutodromoId { get; set; }
        /// <summary>
        /// Instância do autódromo associado à reserva.
        /// </summary>
        public Autodromo Autodromo { get; set; } = null!;

        /// <summary>
        /// ID do utilizador autenticado que fez a reserva (pode ser nulo se anónimo).
        /// </summary>
        public string? UtilizadorId { get; set; }
        /// <summary>
        /// Instância do utilizador autenticado associado à reserva.
        /// </summary>
        public IdentityUser Utilizador { get; set; } = null!;

        /// <summary>
        /// Chave estrangeira da corrida associada à reserva.
        /// </summary>
        [Required]
        public int CorridaId { get; set; }
        /// <summary>
        /// Instância da corrida relacionada com a reserva.
        /// </summary>
        public Corrida Corrida { get; set; } = null!;
    }
}

