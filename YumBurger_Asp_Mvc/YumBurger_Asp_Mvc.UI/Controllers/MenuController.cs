using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models.ViewModels;

namespace YumBurger_Asp_Mvc.UI.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly YumBurgerContext _db;
        public MenuController(YumBurgerContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var menus = _db.Menus.ToList();
            var extras = _db.Extras.ToList();
            MenusExtrasVM menusExtras = new()
            {
                Extra = extras,
                Menus = menus
            };
            return View(menusExtras);
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            var selectedMenu = _db.Menus.FirstOrDefault(menu => menu.Id == id);
            return View();
        }
    }
}
