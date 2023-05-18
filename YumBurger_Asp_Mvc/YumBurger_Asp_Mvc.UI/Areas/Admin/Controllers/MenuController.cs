using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;

namespace YumBurger_Asp_Mvc.UI.Areas.Admin.Views.Home
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MenuController : Controller
    {

        private readonly YumBurgerContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MenuController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: MenuController
        public async Task<ActionResult> Index()
        {
            var menus = await _context.Menus.ToListAsync();
            return View(menus);
        }

        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Menu menu, IFormFile PicturePath, [FromServices] IWebHostEnvironment env)
        {
            if (PicturePath != null && PicturePath.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(PicturePath.FileName);
                var picturesFolder = Path.Combine(env.WebRootPath, "assest", "img", "menuPictures");
                var dosyaYolu = Path.Combine(picturesFolder, dosyaAdi);

                if (!Directory.Exists(picturesFolder))
                {
                    Directory.CreateDirectory(picturesFolder);
                }

                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await PicturePath.CopyToAsync(stream);
                }

                menu.PicturePath = dosyaAdi;
            }

            if (menu is not null)
            {
                Menu createMenu = new()
                {
                    Name = menu.Name,
                    PicturePath = menu.PicturePath,
                    Price = menu.Price,
                    Description = menu.Description,
                };
                await _context.Menus.AddAsync(createMenu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }



        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
