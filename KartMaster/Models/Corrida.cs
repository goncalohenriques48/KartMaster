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
        /// Identificador único da corrida.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da corrida.
        /// </summary>
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Data da corrida.
        /// </summary>
        [Display(Name = "Data da Corrida")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [DataType(DataType.Date)]
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
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int AutodromoId { get; set; }

        /// <summary>
        /// Autódromo onde a corrida será realizada.
        /// </summary>
        [ValidateNever]
        public Autodromo Autodromo { get; set; } = null!;  // Permita nulo para evitar validação desnecessária


        /// <summary>
        /// Lista das participações na corrida.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; } = [];
    }
}

