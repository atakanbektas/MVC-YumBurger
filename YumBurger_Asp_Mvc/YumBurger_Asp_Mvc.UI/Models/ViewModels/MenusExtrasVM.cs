namespace YumBurger_Asp_Mvc.UI.Models.ViewModels
{
    public class MenusExtrasVM
    {
        public MenusExtrasVM()
        {
            Menus = new();
            Extra = new();
        }
        public List<Menu> Menus { get; set; } = null!;
        public List<Extra> Extra { get; set; } = null!;
    }
}
