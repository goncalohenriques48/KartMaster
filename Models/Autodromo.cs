using System.ComponentModel.DataAnnotations;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa um autódromo.
    /// </summary>
    public class Autodromo
    {
        /// <summary>
        /// Identificador único do autódromo.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do autódromo.
        /// </summary>
        public required string Nome { get; set; }

        /// <summary>
        /// Localização do autódromo.
        /// </summary>
        public required string Localizacao { get; set; }

        /// <summary>
        /// Número de telefone do autódromo.
        /// </summary>
        public required string Telemovel { get; set; }

        /// <summary>
        /// Email do autódromo.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Capacidade do autódromo.
        /// </summary>
        public int Capacidade { get; set; }

        /* *************************
        * Definção dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das corridas realizadas no autódromo.
        /// </summary>
        public ICollection<Corrida>? Corridas { get; set; }
    }
}

