using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;
using YumBurger_Asp_Mvc.UI.Models.Enums;

namespace YumBurger_Asp_Mvc.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class FinanceController : Controller
    {

        private readonly YumBurgerContext _context;
        private readonly UserManager<AppUser> _userManager;
        public FinanceController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrdersMenus).ThenInclude(om => om.Menu)
                .Include(o => o.OrdersExtras).ThenInclude(oe => oe.Extra)
                .ToListAsync();
            var selectedOrders = orders.Where(o => o.OrderStatus != OrderStatus.InChart).ToList();
            ViewBag.TotalPrice = selectedOrders.Sum(o => o.TotalPrice);
            return View(selectedOrders);
        }


        [HttpPost]
        public async Task<IActionResult> Index(DateTime? start, DateTime? stop)
        {
            if (start is null)
            {
                start = DateTime.MinValue;
            }
            if (stop is null)
            {
                stop = DateTime.MaxValue;
            }
            var orders = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrdersMenus).ThenInclude(om => om.Menu)
                .Include(o => o.OrdersExtras).ThenInclude(oe => oe.Extra)
                .ToListAsync();

            var selectedOrders = orders.Where(o => o.OrderDate < stop && o.OrderDate > start && o.OrderDate != null && o.OrderStatus != OrderStatus.InChart).ToList();


            ViewBag.TotalPrice = selectedOrders.Sum(o => o.TotalPrice);
            return View(selectedOrders);
        }
    }
}
