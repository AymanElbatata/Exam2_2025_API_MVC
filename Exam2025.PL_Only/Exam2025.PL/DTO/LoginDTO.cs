using System.ComponentModel.DataAnnotations;

namespace Exam2025.PL.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length is 6 chars")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\S)[a-zA-Z\S]{8,100}$", ErrorMessage = "Password Must contain one upper letter and one lower letter.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;

    }
}
