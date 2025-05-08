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
        /// Identificador �nico do utilizador.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador.
        /// </summary>
        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Email do utilizador.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O {0} � de preenchimento obrigat�rio")]
        [EmailAddress(ErrorMessage = "O {0} n�o tem um formato v�lido")]
        [StringLength(100, ErrorMessage = "O {0} n�o pode ter mais do que {1} caracteres")]
        public string Email { get; set; } = string.Empty;



        /// <summary>
        /// Este atributo servir� para fazer a 'ponte' 
        /// entre a tabela dos Utilizadores e a 
        /// tabela da Autentica��o da Microsoft Identity
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [ForeignKey("IdentityUser")]
        public string? IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }


        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das participa��es do utilizador, em corridas.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; } = [];
    }
}

