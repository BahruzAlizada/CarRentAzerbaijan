using CarRentAzerbaijan.ViewModels;
using CarRentAzerbaijan.ViewModels.WebUserVM;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public UserController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await userManager.Users.Where(x=>x.UserRole==null).ToListAsync();
            List<UserVM> userVMs = new List<UserVM>();

            foreach (var item in users)
            {
                UserVM vM = new UserVM
                {
                    Id=item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    UserRole = (await userManager.GetRolesAsync(item))[0],
                    IsDeactive =item.IsDeactive
                };
                userVMs.Add(vM);
            }

            return View(userVMs);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company")).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(UserCreateVM register,int roleId)
        {
            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company")).ToListAsync();
            AppRole role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            register.Username = Guid.NewGuid().ToString("N").Substring(0, 8);

            AppUser user = new AppUser
            {
                UserName = register.Username,
                Name = register.Name,
                Email = register.Email,
                IsDeactive = false,
            };

            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            UserUpdateVM updateVM = new UserUpdateVM
            {
                Email = user.Email,
                Name = user.Name,
            };

            return View(updateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, UserUpdateVM userUpdateVM)
        {
            #region Get
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            UserUpdateVM updateVM = new UserUpdateVM
            {
                Email = user.Email,
                Name = user.Name,
            };

            #endregion

            
            user.Name = userUpdateVM.Name;
            user.Email = userUpdateVM.Email;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region RoleChange
        public async Task<IActionResult> RoleChange(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company")).Select(x => x.Name).ToListAsync();

            RoleVM roleVM = new RoleVM
            {
                Role = (await userManager.GetRolesAsync(user))[0]
            };

            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RoleChange(int? id, string role)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Where(x => !x.Name.Contains("Company")).Select(x => x.Name).ToListAsync();

            RoleVM roleVM = new RoleVM
            {
                Role = (await userManager.GetRolesAsync(user))[0]
            };

            await userManager.RemoveFromRoleAsync(user, roleVM.Role);
            await userManager.AddToRoleAsync(user, role);

            return RedirectToAction("Index");
        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(int? id, ResetPasswordVM resetPassword)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            if (user.IsDeactive)
                user.IsDeactive = false;
            else
                user.IsDeactive = true;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Name = user.Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Name = user.Name;

            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
