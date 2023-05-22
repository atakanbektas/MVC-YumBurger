using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;
using YumBurger_Asp_Mvc.UI.Models.Enums;
using YumBurger_Asp_Mvc.UI.Models.ViewModels;

namespace YumBurger_Asp_Mvc.UI.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly YumBurgerContext _db;
        private readonly UserManager<AppUser> _userManager;
        public MenuController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _db.Menus.Where(m => m.IsSellable == true).ToListAsync();
            var extras = await _db.Extras.Where(m => m.IsSellable == true).ToListAsync();
            MenusExtrasVM menusExtras = new()
            {
                Extra = extras,
                Menus = menus
            };
            return View(menusExtras);
        }


        [HttpPost]
        public async Task<IActionResult> Details(int menuId)
        {
            var selectedMenu = await _db.Menus.FirstOrDefaultAsync(menu => menu.Id == menuId);
            return View(selectedMenu);
        }

        [HttpPost]
        public async Task<IActionResult> AddChart(Menu menu, int quantity = 1)
        {

            var user = await _userManager.GetUserAsync(User);
            var orderInChart = await _db.Orders.Where(o => o.AppUserId == user.Id && o.OrderStatus == OrderStatus.InChart).FirstOrDefaultAsync();

            if (orderInChart != null) // exist order
            {
                orderInChart.TotalPrice += menu.Price * quantity;


                OrdersMenu ordersMenu = new()
                {
                    Order = orderInChart,
                    Menu = menu,
                    Quantity = quantity
                };

                orderInChart.OrdersMenus.Add(ordersMenu);
                await _userManager.UpdateAsync(user);
            }
            else // doesn't exist order
            {
                var order = new Order()
                {
                    AppUser = user,
                    UserAddress = user.Address,
                };
                order.TotalPrice += menu.Price * quantity;

                OrdersMenu ordersMenu = new()
                {
                    Order = order,
                    Menu = menu,
                    Quantity = quantity
                };

                order.OrdersMenus.Add(ordersMenu);
                user.Orders.Add(order);
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Menu");
        }
    }
}
