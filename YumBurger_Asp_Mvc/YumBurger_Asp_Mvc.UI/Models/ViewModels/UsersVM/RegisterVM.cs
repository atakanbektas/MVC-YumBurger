using System.ComponentModel.DataAnnotations;

namespace YumBurger_Asp_Mvc.UI.Models.ViewModels.UsersVM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]

        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field is required.")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Lesser than be 20 char")]
        [MinLength(8, ErrorMessage = "Greater than be 8 char")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
