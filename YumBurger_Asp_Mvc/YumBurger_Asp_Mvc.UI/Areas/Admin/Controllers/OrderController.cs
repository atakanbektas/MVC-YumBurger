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
    public class OrderController : Controller
    {

        private readonly YumBurgerContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("OnTheWay");
        }
        public async Task<IActionResult> OnTheWay()
        {
            // select orders on the way
            var orders = await _context.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).ThenInclude(om => om.Menu).Include(o => o.OrdersExtras).ThenInclude(oe => oe.Extra).Where(o => o.OrderStatus == OrderStatus.OnWay).ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeComplete(int id)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == id);
            order.OrderStatus = OrderStatus.Delivered;
            order.ActualArrivalDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("OnTheWay");
        }

        public async Task<IActionResult> Delivered()
        {
            // select orders delivered
            var orders = await _context.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).ThenInclude(om => om.Menu).Include(o => o.OrdersExtras).ThenInclude(oe => oe.Extra).Where(o => o.OrderStatus == OrderStatus.Delivered).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            // select order with menu and extra
            var order = await _context.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).ThenInclude(om => om.Menu).Include(o => o.OrdersExtras).ThenInclude(oe => oe.Extra).FirstAsync(o => o.Id == id);
            return View(order);
        }
    }
}
