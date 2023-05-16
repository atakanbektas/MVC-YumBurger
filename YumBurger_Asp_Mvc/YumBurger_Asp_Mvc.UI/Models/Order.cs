using YumBurger_Asp_Mvc.UI.Models.Enums;

namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersExtras = new HashSet<OrdersExtra>();
            OrdersMenus = new HashSet<OrdersMenu>();
            OrderStatus = OrderStatus.InChart;
        }

        public int Id { get; set; }
        public string AppUserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? UserAddress { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;
        public virtual ICollection<OrdersExtra> OrdersExtras { get; set; }
        public virtual ICollection<OrdersMenu> OrdersMenus { get; set; }
    }
}
