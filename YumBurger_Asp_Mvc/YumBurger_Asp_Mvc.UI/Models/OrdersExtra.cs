namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class OrdersExtra
    {
        public int OrderId { get; set; }
        public int ExtraId { get; set; }
        public int Quantity { get; set; }
        public virtual Extra Extra { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
