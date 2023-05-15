namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class Extra
    {
        public Extra()
        {
            OrdersExtras = new HashSet<OrdersExtra>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PicturePath { get; set; }
        public virtual ICollection<OrdersExtra> OrdersExtras { get; set; }
    }
}
