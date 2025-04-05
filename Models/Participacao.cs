using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa a participação de um utilizador em uma corrida.
    /// </summary>
    public class Participacao
    {
        /* *************************
        * Definção dos relacionamentos
        * ************************** 
        */

        // Relacionamentos N-N

        /// <summary>
        /// FK para referenciar o utilizador.
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorId { get; set; }

        /// <summary>
        /// Utilizador que participou da corrida.
        /// </summary>
        public Utilizador Utilizador { get; set; }

        /// <summary>
        /// FK para referenciar a corrida.
        /// </summary>
        [ForeignKey(nameof(Corrida))]
        public int CorridaId { get; set; }

        /// <summary>
        /// Corrida em que o utilizador participou.
        /// </summary>
        public Corrida Corrida { get; set; }

        /// <summary>
        /// Posição final do utilizador na corrida.
        /// </summary>
        public string PosicaoFinal { get; set; }

        /// <summary>
        /// Tempo final do utilizador na corrida.
        /// </summary>
        public TimeSpan TempoFinal { get; set; }

        /// <summary>
        /// Chave primária composta para a participação.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int ParticipacaoId { get; set; }
    }
}

