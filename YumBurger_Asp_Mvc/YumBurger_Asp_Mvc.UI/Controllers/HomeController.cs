using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;
using YumBurger_Asp_Mvc.UI.Models.ViewModels;

namespace YumBurger_Asp_Mvc.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YumBurgerContext _context;
        public HomeController(ILogger<HomeController> logger, YumBurgerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var menus = _context.Menus.Where(m => m.IsSellable == true).ToList();
            var extras = _context.Extras.Where(m => m.IsSellable == true).ToList();


            MenusExtrasVM menusExtras = new();

            for (int i = 0; i < 6; i++)
            {
                menusExtras.Menus.Add(menus[i]);
                menusExtras.Extra.Add(extras[i]);
            }

            return View(menusExtras);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}