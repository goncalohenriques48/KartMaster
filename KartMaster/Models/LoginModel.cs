using System.ComponentModel.DataAnnotations;

namespace KartMaster.Models {
    /// <summary>
    /// Modelo utilizado para autenticação de utilizadores no sistema.
    /// </summary>
    public class LoginModel {
        /// <summary>
        /// Nome de utilizador (username) para login.
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Palavra-passe do utilizador.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}