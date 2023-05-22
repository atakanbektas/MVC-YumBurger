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
        public async Task<ActionResult> Edit(int id)
        {
            var editMenu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);
            if (editMenu is not null)
            {
                return View(editMenu);
            }
            return RedirectToAction("Index"); // if edit menu is null..
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, [FromServices] IWebHostEnvironment env)
        {

            string picturePath;
            IFormFile picture;


            var editMenu = await _context.Menus.FirstAsync(m => m.Id == id);

            editMenu.Name = collection["Name"];
            editMenu.Price = Convert.ToDecimal(collection["Price"]);
            editMenu.Description = collection["Description"];
            editMenu.IsSellable = collection["IsSellable"] == "false" ? false : true; // temprory solition


            if (collection.Files.Count > 0)
            {
                picture = collection.Files[0];
                picturePath = picture.FileName;
                if (picturePath is not null && picturePath.Length > 0)
                {
                    editMenu.PicturePath = picturePath;
                    var picturesFolder = Path.Combine(env.WebRootPath, "assest", "img", "menuPictures");
                    var dosyaYolu = Path.Combine(picturesFolder, picturePath);

                    if (!Directory.Exists(picturesFolder))
                    {
                        Directory.CreateDirectory(picturesFolder);
                    }

                    using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // POST: MenuController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var changeSellableMenu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);

            if (changeSellableMenu is not null)
            {
                changeSellableMenu.IsSellable = changeSellableMenu.IsSellable ? false : true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Menu");
        }
    }
}
