using CarRentAzerbaijan.ViewModels;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Helper;
using NuGet.Protocol.Plugins;

namespace CarRentAzerbaijan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IWebHostEnvironment env;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.signInManager=signInManager;
            this.roleManager = roleManager;
            this.env = env;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser? user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email yaxud Şifrə yanlışdır");
                return View();
            }
            if (user.IsDeactive)
            {
                ModelState.AddModelError("", "Sizin hesabınız admin tərəfdən bloklanıb");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.IsRemember, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email yaxud şifrə yanlışdır");
                return View();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Sizin hesabınız qısa müddətlik bloklanıb. 1 dəqiqə sonra yenidən yoxlayın");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            #region Image
            if (registerVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
                return View();
            }
            if (!registerVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sadəcə jpeg yaxud jpg tipli fayllar");
                return View();
            }
            if (registerVM.Photo.IsOlder512Kb())
            {
                ModelState.AddModelError("Photo", "Maksimum 512 Kb");
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "images", "users");
            #endregion

            registerVM.Username = Guid.NewGuid().ToString("N").Substring(0, 8);

            AppUser user = new AppUser
            {
                Email = registerVM.Email,
                UserName= registerVM.Username,
                Name = registerVM.Name,
                Image = await registerVM.Photo.SaveFileAsync(folder),
                UserRole="Company"
            };

            IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }


            var roleName = await roleManager.FindByNameAsync("Company");
            await userManager.AddToRoleAsync(user, roleName.Name);
            await signInManager.SignInAsync(user, registerVM.IsRemember);

            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
