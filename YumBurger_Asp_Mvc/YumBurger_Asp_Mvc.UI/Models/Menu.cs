using YumBurger_Asp_Mvc.UI.Models.Enums;

namespace YumBurger_Asp_Mvc.UI.Models
{
    public partial class Menu
    {
        public Menu()
        {
            OrdersMenus = new HashSet<OrdersMenu>();
            CokePatotoSize = CokePotatoSize.Small;
            Description = "This product does not have a description.";

        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CokePotatoSize CokePatotoSize { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PicturePath { get; set; }

        public virtual ICollection<OrdersMenu> OrdersMenus { get; set; }
    }
}
