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
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        [StringLength(100, ErrorMessage = "O {0} n�o pode ter mais do que {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// nome da localidade da Localiza��o do Kartot�dromo.
        /// </summary>
        [Display(Name = "Localiza��o")]
        [Required(ErrorMessage = "A {0} � de preenchimento obrigat�rio")]
        [StringLength(100, ErrorMessage = "A {0} n�o pode ter mais do que {1} caracteres")]
        public string Localizacao { get; set; } = string.Empty;

        /// <summary>
        /// N�mero de telefone do aut�dromo.
        /// </summary>
        [Display(Name = "Telem�vel")]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        [StringLength(18, ErrorMessage = "O {0} n�o pode ter mais do que {1} caracteres")]
        [RegularExpression(@"(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um n� de telefone v�lido. Pode adicionar o indicativo do pa�s.")]
        public string Telemovel { get; set; } = string.Empty;

        /// <summary>
        /// Email do aut�dromo.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        [EmailAddress(ErrorMessage = "O {0} n�o tem um formato v�lido")]
        [StringLength(100, ErrorMessage = "O {0} n�o pode ter mais do que {1} caracteres")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// N�mero de karts que podem participar, em simult�neo, numa corrida, no aut�dromo.
        /// </summary>
        [Display(Name = "Capacidade")]
        [Required(ErrorMessage = "A {0} � de preenchimento obrigat�rio")]
        [Range(1, 100, ErrorMessage = "A {0} deve estar entre {1} e {2} karts")]
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

