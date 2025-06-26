using System.ComponentModel.DataAnnotations;

namespace KartMaster.Models {
    public class LoginModel {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}