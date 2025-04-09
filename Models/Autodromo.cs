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
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Localização do autódromo.
        /// </summary>
        public string Localizacao { get; set; } = string.Empty;

        /// <summary>
        /// Número de telefone do autódromo.
        /// </summary>
        public string Telemovel { get; set; } = string.Empty;

        /// <summary>
        /// Email do autódromo.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número de karks que podem participar, em simultâneo, numa corrida, no autódromo.
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
        public ICollection<Corrida> Corridas { get; set; } = [];
    }
}

