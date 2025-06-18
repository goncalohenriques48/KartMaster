using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa a participa��o de um utilizador em uma corrida.
    /// </summary>
    [PrimaryKey(nameof(UtilizadorId), nameof(CorridaId))]
    public class Participacao
    {
        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos N-N

        /// <summary>
        /// FK para referenciar o utilizador.
        /// </summary>
        [Display(Name = "Utilizador")]
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorId { get; set; }

        /// <summary>
        /// Utilizador que participou da corrida.
        /// </summary>
        [ValidateNever]
        public Utilizador? Utilizador { get; set; }


        /// <summary>
        /// FK para referenciar a corrida.
        /// </summary>
        [Display(Name = "Corrida")]
        [ForeignKey(nameof(Corrida))]
        public int CorridaId { get; set; }

        /// <summary>
        /// Corrida em que o utilizador participou.
        /// </summary>
        [ValidateNever]
        public Corrida? Corrida { get; set; }


        /// <summary>
        /// Posi��o final do utilizador na corrida.(ex: "1�", "DNF", etc.)
        /// </summary>
        [Display(Name = "Posi��o Final")]
        [Required(ErrorMessage = "A {0} � obrigat�ria")]
        [RegularExpression(@"^\d+�$", ErrorMessage = "A {0} deve ser um n�mero seguido do s�mbolo � (exemplo: 1�, 2�).")]
        [StringLength(10, ErrorMessage = "A {0} n�o pode ter mais do que {1} caracteres")]
        public string PosicaoFinal { get; set; } = string.Empty;

        /// <summary>
        /// Tempo final do utilizador na corrida.
        /// </summary>
        [Display(Name = "Tempo Final")]
        [Required(ErrorMessage = "O {0} � obrigat�rio")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "O {0} deve estar no formato hh:mm:ss (exemplo: 10:30:45)")]
        public TimeSpan TempoFinal { get; set; }


    }
}

