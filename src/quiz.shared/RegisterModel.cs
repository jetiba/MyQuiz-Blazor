using System.ComponentModel.DataAnnotations;

namespace quiz.shared
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} deve contenere minimo {2} e massimo {1} caratteri", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "Le due password non corrispondono")]
        public string ConfirmPassword { get; set; }
    }
}
