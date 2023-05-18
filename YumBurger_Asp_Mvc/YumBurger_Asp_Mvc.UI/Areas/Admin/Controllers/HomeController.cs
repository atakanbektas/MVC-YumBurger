using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;

namespace YumBurger_Asp_Mvc.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly YumBurgerContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            ViewBag.Email = GetUserEmailAsync();
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task<string> GetUserEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Email;
        }
    }
}
