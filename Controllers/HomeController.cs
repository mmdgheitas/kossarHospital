using kosarHospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace kosarHospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DB db;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            DbContextOptionsBuilder<DB> optionsBuilder = new DbContextOptionsBuilder<DB>();
            db = new DB(optionsBuilder.Options);
        }

        public async Task<IActionResult> login_singin(string username, string PasswordHash)
        {
            if (username != null)
            {
                var h = await userManager.FindByNameAsync(username);
                if (h == null)
                {
                    return Json("یافت نشد");
                }
                if (PasswordHash != null)
                {
                    var s = await signInManager.PasswordSignInAsync(user: h, password: PasswordHash, true, false);

                    if (s.Succeeded)
                    {
                        await signInManager.SignInAsync(h, true);
                        return RedirectToAction("profile");
                    }
                    else
                        return RedirectToAction("login_singin");
                }
                else
                {
                    ModelState.AddModelError("", "پسورد را وارد کنید");
                    return View("login_signin");
                }
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Signin(User h, string roleName)
        {
            var resault_user = await userManager.CreateAsync(h, h.PasswordHash);
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            var resault_role = await userManager.AddToRoleAsync(h, roleName);
            if (resault_user.Succeeded && resault_role.Succeeded)
                if (resault_user.Succeeded)
                {
                    await signInManager.SignInAsync(h, true);
                    return RedirectToAction("profile");
                }
                else
                {
                    return RedirectToAction("login_singin", "مشکلی رخ داده است");
                }
            return View();
        }

        //[Authorize]
        public async Task<IActionResult> profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var r = await userManager.IsInRoleAsync(user, "admin");
                if (r == true)
                {
                    user.times = db.Time.ToList();
                    return View(user);
                }
                user.times = db.Time.Where(i => i.user.Id == user.Id).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("login_singin");
            }
        }

        public async Task<IActionResult> reserve(Time time)
        {
            time.user = await userManager.FindByNameAsync(User.Identity.Name);
            db.Attach(time.user);
            db.Time.Add(time);
            db.SaveChanges();
            return RedirectToAction("profile");
        }

        public IActionResult search(string data)
        {
            var user = userManager.Users.Where(i => i.codemeli == data).SingleOrDefault();
            if (user != null)
            {
                user.times = db.Time.Where(i => i.user.Id == user.Id).ToList();
                return View("profile", user);
            }
            else
            {
                ViewBag.error = "کاربری با این کد ملی یافت نشد";
                return View("profile", user);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}