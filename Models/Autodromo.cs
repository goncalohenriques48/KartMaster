using System.ComponentModel.DataAnnotations;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa um aut�dromo.
    /// </summary>
    public class Autodromo
    {
        /// <summary>
        /// Identificador �nico do aut�dromo.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do aut�dromo.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Localiza��o do aut�dromo.
        /// </summary>
        public string Localizacao { get; set; } = string.Empty;

        /// <summary>
        /// N�mero de telefone do aut�dromo.
        /// </summary>
        public string Telemovel { get; set; } = string.Empty;

        /// <summary>
        /// Email do aut�dromo.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// N�mero de karks que podem participar, em simult�neo, numa corrida, no aut�dromo.
        /// </summary>
        public int Capacidade { get; set; }

        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das corridas realizadas no aut�dromo.
        /// </summary>
        public ICollection<Corrida> Corridas { get; set; } = [];
    }
}

