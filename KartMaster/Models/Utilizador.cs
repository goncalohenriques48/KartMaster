using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa um utilizador.
    /// </summary>
    public class Utilizador
    {
        /// <summary>
        /// Identificador único do utilizador.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador.
        /// </summary>
        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Email do utilizador.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "O {0} não tem um formato válido")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres")]
        public string Email { get; set; } = string.Empty;



        /// <summary>
        /// Este atributo servirá para fazer a 'ponte' 
        /// entre a tabela dos Utilizadores e a 
        /// tabela da Autenticação da Microsoft Identity
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [ForeignKey("IdentityUser")]
        public string? IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }


        /* *************************
        * Definção dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das participações do utilizador, em corridas.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; } = [];
    }
}

