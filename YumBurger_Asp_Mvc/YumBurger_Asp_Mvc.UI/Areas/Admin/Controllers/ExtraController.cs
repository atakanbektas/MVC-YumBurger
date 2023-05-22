using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Data;
using YumBurger_Asp_Mvc.UI.Models;

namespace YumBurger_Asp_Mvc.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ExtraController : Controller
    {
        private readonly YumBurgerContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ExtraController(YumBurgerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: ExtraController
        public async Task<ActionResult> Index()
        {
            var extras = await _context.Extras.ToListAsync();
            return View(extras);
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
        public async Task<IActionResult> Create(Extra extra, IFormFile PicturePath, [FromServices] IWebHostEnvironment env)
        {
            if (PicturePath != null && PicturePath.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(PicturePath.FileName);
                var picturesFolder = Path.Combine(env.WebRootPath, "assest", "img", "ExtrasPictures");
                var dosyaYolu = Path.Combine(picturesFolder, dosyaAdi);

                if (!Directory.Exists(picturesFolder))
                {
                    Directory.CreateDirectory(picturesFolder);
                }

                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await PicturePath.CopyToAsync(stream);
                }

                extra.PicturePath = dosyaAdi;
            }

            if (extra is not null)
            {
                Extra createExtra = new()
                {
                    Name = extra.Name,
                    PicturePath = extra.PicturePath,
                    Price = extra.Price,
                    Description = extra.Description,
                };
                await _context.Extras.AddAsync(createExtra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }



        // GET: MenuController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var editExtra = await _context.Extras.FirstOrDefaultAsync(m => m.Id == id);
            if (editExtra is not null)
            {
                return View(editExtra);
            }
            return RedirectToAction("Index"); // if edit extra is null..
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, [FromServices] IWebHostEnvironment env)
        {
            string picturePath;
            IFormFile picture;


            var editExtra = await _context.Extras.FirstAsync(m => m.Id == id);

            editExtra.Name = collection["Name"];
            editExtra.Price = Convert.ToDecimal(collection["Price"]);
            editExtra.Description = collection["Description"];
            editExtra.IsSellable = collection["IsSellable"] == "false" ? false : true; // temprory solition


            if (collection.Files.Count > 0)
            {
                picture = collection.Files[0];
                picturePath = picture.FileName;
                if (picturePath is not null && picturePath.Length > 0)
                {
                    editExtra.PicturePath = picturePath;
                    var picturesFolder = Path.Combine(env.WebRootPath, "assest", "img", "ExtrasPictures");
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
            var changeSellableExtra = await _context.Extras.FirstOrDefaultAsync(m => m.Id == id);

            if (changeSellableExtra is not null)
            {
                changeSellableExtra.IsSellable = changeSellableExtra.IsSellable ? false : true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Extra");
        }
    }
}
