using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.IsSellable == true).ToListAsync();
            var extras = await _context.Extras.Where(m => m.IsSellable == true).ToListAsync();


            MenusExtrasVM menusExtras = new();

            for (int i = 0; i < 4; i++)
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