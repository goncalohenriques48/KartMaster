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
        public required string Nome { get; set; }

        /// <summary>
        /// Localiza��o do aut�dromo.
        /// </summary>
        public required string Localizacao { get; set; }

        /// <summary>
        /// N�mero de telefone do aut�dromo.
        /// </summary>
        public required string Telemovel { get; set; }

        /// <summary>
        /// Email do aut�dromo.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Capacidade do aut�dromo.
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
        public ICollection<Corrida>? Corridas { get; set; }
    }
}

