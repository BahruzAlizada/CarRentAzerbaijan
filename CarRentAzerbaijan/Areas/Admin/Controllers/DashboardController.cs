using BusinessLayer.Abstract;
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
        public DashboardController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #endregion
    }
}
