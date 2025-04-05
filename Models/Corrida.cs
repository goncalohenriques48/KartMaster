using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa uma corrida.
    /// </summary>
    public class Corrida
    {
        /// <summary>
        /// Identificador �nico da corrida.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da corrida.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data da corrida.
        /// </summary>
        public DateTime Data { get; set; }

        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// FK para referenciar o aut�dromo onde a corrida ser� realizada.
        /// </summary>
        [ForeignKey(nameof(Autodromo))]
        public int AutodromoId { get; set; }

        /// <summary>
        /// Aut�dromo onde a corrida ser� realizada.
        /// </summary>
        public Autodromo Autodromo { get; set; }

        /// <summary>
        /// Lista das participa��es na corrida.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; }
    }
}

