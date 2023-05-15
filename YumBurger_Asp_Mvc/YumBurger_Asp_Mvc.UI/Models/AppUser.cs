using Microsoft.AspNetCore.Identity;

namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class AppUser : IdentityUser
    {
        public string Address { get; set; } = null!;
        public string? PicturePath { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
