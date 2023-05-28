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
            var deletedOrder = _db.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).Include(o => o.OrdersExtras).FirstOrDefault(o => o.AppUser == user && o.OrderStatus == OrderStatus.InChart);
            if (deletedOrder != null)
            {
                _db.Orders.Remove(deletedOrder);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Order");
        }



        public async Task<IActionResult> DeleteMenu(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var order = _db.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).Include(o => o.OrdersExtras).FirstOrDefault(o => o.AppUser == user && o.OrderStatus == OrderStatus.InChart);

            if (order is not null)
            {
                var deletedOrdersMenu = await _db.OrdersMenus.Include(om => om.Menu).FirstOrDefaultAsync(om => om.MenuId == id);
                if (deletedOrdersMenu is not null)
                {
                    order.TotalPrice -= deletedOrdersMenu.Menu.Price * deletedOrdersMenu.Quantity;
                    order.OrdersMenus.Remove(deletedOrdersMenu);
                    if (order.OrdersExtras.Count == 0 && order.OrdersMenus.Count == 0)
                    {
                        _db.Orders.Remove(order);
                    }
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteExtra(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var order = await _db.Orders.Include(o => o.AppUser).Include(o => o.OrdersMenus).Include(o => o.OrdersExtras).FirstOrDefaultAsync(o => o.AppUser == user && o.OrderStatus == OrderStatus.InChart);

            if (order != null)
            {
                var deletedOrdersExtra = await _db.OrdersExtras.Include(oe => oe.Extra).FirstOrDefaultAsync(oe => oe.ExtraId == id);
                if (deletedOrdersExtra is not null)
                {
                    order.TotalPrice -= deletedOrdersExtra.Extra.Price * deletedOrdersExtra.Quantity;
                    order.OrdersExtras.Remove(deletedOrdersExtra);
                    if (order.OrdersExtras.Count == 0 && order.OrdersMenus.Count == 0)
                    {
                        _db.Orders.Remove(order);
                    }
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Buy(int Id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == Id);

            if (order != null)
            {
                order.OrderStatus = OrderStatus.OnWay;
                order.OrderDate = DateTime.Now;
                order.EstimatedArrivalDate = DateTime.Now.AddMinutes(45);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
