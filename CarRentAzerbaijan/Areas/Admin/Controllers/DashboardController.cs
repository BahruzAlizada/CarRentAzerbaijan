using BusinessLayer.Abstract;
using CarRentAzerbaijan.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobEntry.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly Context context;
        public DashboardController(UserManager<AppUser> userManager, Context context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            StatsModel model = new StatsModel
            {
                MarkaCount = await context.Models.Where(x => x.IsMain).CountAsync(),
                ModelCount = await context.Models.Where(x => !x.IsMain).CountAsync(),
                CompanyCount = await userManager.Users.Where(x => x.UserRole.Contains("Company")).CountAsync()
            };

            return View(model);
        }
        #endregion
    }
}
