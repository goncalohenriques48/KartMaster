using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        /// Hora de in�cio da corrida.
        /// </summary>
        [Display(Name = "Hora")]
        [Required(ErrorMessage = "A {0} � obrigat�ria")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Dura��o estimada da corrida.
        /// </summary>
        [Display(Name = "Dura��o")]
        [Required(ErrorMessage = "A {0} � obrigat�ria")]
        [DataType(DataType.Time)]
        public TimeSpan Duracao { get; set; }

        /// <summary>
        /// Identificador �nico da corrida.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da corrida.
        /// </summary>
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        [StringLength(50, ErrorMessage = "O {0} n�o pode ter mais do que {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Data da corrida.
        /// </summary>
        [Display(Name = "Data da Corrida")]
        [Required(ErrorMessage = "A {0} � de preenchimento obrigat�rio")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// FK para referenciar o aut�dromo onde a corrida ser� realizada.
        /// </summary>
        [Display(Name = "Aut�dromo")]
        [ForeignKey(nameof(Autodromo))]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        public int AutodromoId { get; set; }

        /// <summary>
        /// Aut�dromo onde a corrida ser� realizada.
        /// </summary>
        [ValidateNever]
        public Autodromo Autodromo { get; set; } = null!;  // Permita nulo para evitar valida��o desnecess�ria


        /// <summary>
        /// Lista das participa��es na corrida.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; } = [];
    }
}

