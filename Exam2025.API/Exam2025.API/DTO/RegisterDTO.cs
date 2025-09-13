using System.ComponentModel.DataAnnotations;

namespace Exam2025.API.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\S)[a-zA-Z\S]{8,100}$", ErrorMessage = "Password Must contain one upper letter and one lower letter.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [MaxLength(10, ErrorMessage = "First Name must be at max 10 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [MaxLength(10, ErrorMessage = "Last Name must be at max 10 characters")]
        public string LastName { get; set; }

    }
}
