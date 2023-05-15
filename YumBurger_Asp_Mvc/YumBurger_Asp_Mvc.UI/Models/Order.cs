namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersExtras = new HashSet<OrdersExtra>();
            OrdersMenus = new HashSet<OrdersMenu>();
        }
        public int Id { get; set; }
        public string ShoppingCartId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? UserAddress { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
        public virtual ICollection<OrdersExtra> OrdersExtras { get; set; }
        public virtual ICollection<OrdersMenu> OrdersMenus { get; set; }
    }
}
