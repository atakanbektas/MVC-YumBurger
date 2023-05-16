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
    public class ExtraController : Controller
    {
        private readonly YumBurgerContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ExtraController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public IActionResult Details(int extraId)
        {
            var selectedExtra = _db.Extras.FirstOrDefault(ext => ext.Id == extraId);
            return View(selectedExtra);
        }

        [HttpPost]
        public async Task<IActionResult> AddChart(Extra extra, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            //var orderInChart = user.Orders.FirstOrDefault(o => o.OrderStatus == OrderStatus.InChart);
            var orderInChart = await _db.Orders.Where(o => o.AppUserId == user.Id && o.OrderStatus == OrderStatus.InChart).FirstOrDefaultAsync();



            if (orderInChart != null) // exist order
            {
                orderInChart.TotalPrice += extra.Price;
                var ordersExtra = new OrdersExtra()
                {
                    Order = orderInChart,
                    Extra = extra,
                    Quantity = quantity
                };

                orderInChart.OrdersExtras.Add(ordersExtra);
                await _userManager.UpdateAsync(user);
            }
            else // doesn't exist order
            {
                var order = new Order()
                {
                    AppUser = user,
                    UserAddress = user.Address,
                };
                order.TotalPrice += extra.Price;

                var ordersExtra = new OrdersExtra()
                {
                    Order = order,
                    Extra = extra,
                    Quantity = quantity
                };

                order.OrdersExtras.Add(ordersExtra);
                user.Orders.Add(order);
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Menu");
        }
    }
}
