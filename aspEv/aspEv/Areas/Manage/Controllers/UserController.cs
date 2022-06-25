using aspEv.Dal;
using aspEv.Models;
using aspEv.Utilites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aspEv.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class UserController : Controller
    {
        private AppDbContext _context { get; }

        private readonly IWebHostEnvironment _env;

        public UserController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<User> user = _context.users.ToList();
            return View(user);
        }
        public async Task <IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool users = _context.users.Any(x => x.Name == user.Name);
            if (users)
            {
                return View();

            }
            if (!user.Photo.CheckSize(200))
            {
                ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                return View();
            }
            if (!user.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                return View();


            }
            user.ImgUrl = await user.Photo.SavaChangeAsync(Path.Combine(_env.WebRootPath, "assest", "image"));
         await   _context.users.AddAsync(user);
          await  _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            User user = _context.users.Find(Id);
            if (user==null)
            {
                return BadRequest();

            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Update(int Id,User user)
        {

            User user1 = _context.users.Find(Id);
            if (user1 == null)
            {
                return BadRequest();

            }
           
            user1.Degre = user.Degre;
            user1.ImgUrl = user.ImgUrl;
            user1.Raiting = user.Raiting;
            user1.SocialName = user.SocialName;
            user1.Name = user1.Name;
            if (user1.ImgUrl!=null)
            {

                if (!user.Photo.CheckSize(200))
                {
                    ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                    return View();
                }
                if (!user.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                    return View();


                }
                user.ImgUrl = await user.Photo.SavaChangeAsync(Path.Combine(_env.WebRootPath, "assest", "image"));


            }
            else
            {
                ModelState.AddModelError("", "sekil elave edin");
                return View();
            }
            await   _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            User user = _context.users.Find(Id);
            if (user==null)
            {
                return NotFound();

            }
            _context.users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
