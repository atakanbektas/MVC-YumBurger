namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class Menu
    {
        public Menu()
        {
            OrdersMenus = new HashSet<OrdersMenu>();
            Description = "This product does not have a description.";
            IsSellable = true;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PicturePath { get; set; }
        public bool IsSellable { get; set; }

        public virtual ICollection<OrdersMenu> OrdersMenus { get; set; }
    }
}
