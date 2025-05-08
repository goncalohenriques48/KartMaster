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
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// nome da localidade da Localização do Kartotódromo.
        /// </summary>
        [Display(Name = "Localização")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} não pode ter mais do que {1} caracteres")]
        public string Localizacao { get; set; } = string.Empty;

        /// <summary>
        /// Número de telefone do autódromo.
        /// </summary>
        [Display(Name = "Telemóvel")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(18, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres")]
        [RegularExpression(@"(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um nº de telefone válido. Pode adicionar o indicativo do país.")]
        public string Telemovel { get; set; } = string.Empty;

        /// <summary>
        /// Email do autódromo.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "O {0} não tem um formato válido")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número de karts que podem participar, em simultâneo, numa corrida, no autódromo.
        /// </summary>
        [Display(Name = "Capacidade")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Range(1, 100, ErrorMessage = "A {0} deve estar entre {1} e {2} karts")]
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

