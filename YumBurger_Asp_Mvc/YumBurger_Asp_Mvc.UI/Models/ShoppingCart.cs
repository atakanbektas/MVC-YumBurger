namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; } = null!;
        public virtual AppUser AppUser { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
