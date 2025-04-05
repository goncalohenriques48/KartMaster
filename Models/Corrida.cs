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
        /// Identificador único da corrida.
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
        * Definção dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// FK para referenciar o autódromo onde a corrida será realizada.
        /// </summary>
        [ForeignKey(nameof(Autodromo))]
        public int AutodromoId { get; set; }

        /// <summary>
        /// Autódromo onde a corrida será realizada.
        /// </summary>
        public Autodromo Autodromo { get; set; }

        /// <summary>
        /// Lista das participações na corrida.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; }
    }
}

