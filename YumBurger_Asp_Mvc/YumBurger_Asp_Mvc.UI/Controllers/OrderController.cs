using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;
using YumBurger_Asp_Mvc.UI.Models.Enums;

namespace YumBurger_Asp_Mvc.UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly YumBurgerContext _db;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //var orderInChart = await _db.Orders.Where(o => o.AppUserId == user.Id && o.OrderStatus == OrderStatus.InChart).FirstOrDefaultAsync();

            var orderInChart = await _db.Orders.Include(o => o.OrdersMenus).ThenInclude(om => om.Menu).Include(o => o.OrdersExtras).ThenInclude(od => od.Extra).FirstOrDefaultAsync(o => o.AppUserId == user.Id && o.OrderStatus == OrderStatus.InChart);


            if (orderInChart is not null)
            {
                return View(orderInChart);
            }
            return View(null);
        }

        public async Task<IActionResult> ClearAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var deletedOrder = await _db.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.AppUser == user);
            if (deletedOrder != null)
            {
                user.Orders.Remove(deletedOrder);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Menu");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var order = user.Orders.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                var deletedMenu = _db.Menus.FirstOrDefaultAsync(m => m.Id == id);
            }
            return RedirectToAction("Index");
        }
    }
}
