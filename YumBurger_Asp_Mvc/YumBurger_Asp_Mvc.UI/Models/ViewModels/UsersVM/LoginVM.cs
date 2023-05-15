using System.ComponentModel.DataAnnotations;

namespace YumBurger_Asp_Mvc.UI.Models.ViewModels.UsersVM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
