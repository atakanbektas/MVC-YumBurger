using Microsoft.AspNetCore.Identity;

namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class AppUser : IdentityUser
    {
        public AppUser()
        {
            Orders = new HashSet<Order>();
            Address = "Lorem Ipsum Dollar!";
        }
        public string Address { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
