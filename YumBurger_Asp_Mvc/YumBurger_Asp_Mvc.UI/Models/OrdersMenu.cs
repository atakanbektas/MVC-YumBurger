namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class OrdersMenu
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public virtual Menu Menu { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
