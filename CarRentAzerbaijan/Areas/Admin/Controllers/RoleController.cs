using CarRentAzerbaijan.ViewModels;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobEntry.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        #region Role
        public IActionResult Index()
        {
            List<AppRole> roles = roleManager.Roles.ToList();
            List<RoleVM> roleVMs = new List<RoleVM>();
            foreach (var item in roles)
            {
                RoleVM vm = new RoleVM
                {
                    Id = item.Id,
                    Role = item.Name,
                    RoleNormalizied=item.NormalizedName
                };
                roleVMs.Add(vm);
            }

            return View(roleVMs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            #region IsExist
            bool isExist = roleManager.Roles.ToList().Any(x => x.Name == roleVM.Role);
            if (isExist)
            {
                ModelState.AddModelError("Role", "Bu Rol hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            AppRole appRole = new AppRole
            {
                Id = roleVM.Id,
                Name = roleVM.Role,
                NormalizedName = roleVM.Role.ToUpper()
            };

            await roleManager.CreateAsync(appRole);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            AppRole? role = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null) return BadRequest();

            RoleVM dbroleVM = new RoleVM
            {
                Id = role.Id,
                Role = role.Name,
                RoleNormalizied = role.NormalizedName
            };

            return View(dbroleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,RoleVM roleVM )
        {
            #region Get
            if (id == null) return NotFound();
            AppRole? role = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null) return BadRequest();

            RoleVM dbroleVM = new RoleVM
            {
                Id = role.Id,
                Role = role.Name,
                RoleNormalizied = role.NormalizedName
            };
            #endregion

            #region IsExist
            bool isExist = roleManager.Roles.ToList().Any(x => x.Name == roleVM.Role && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Role", "Bu Rol hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            role.Id = roleVM.Id;
            role.Name = roleVM.Role;
            role.NormalizedName= roleVM.Role.ToUpper();

            await roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
