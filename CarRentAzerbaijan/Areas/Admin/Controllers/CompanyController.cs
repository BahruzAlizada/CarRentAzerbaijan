using BusinessLayer.Abstract;
using CarRentAzerbaijan.ViewModels;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly UserManager<AppUser> userManager;
        public CompanyController(ICompanyService companyService,UserManager<AppUser> userManager)
        {
            this.companyService = companyService;
            this.userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = await companyService.AllCompaniesPageCountAsync(page);
            ViewBag.CurrentPage = page;

            List<CompanyDto> companies = await companyService.GetAllCompaniesWithPagingAsync((int)take,page);
            ViewBag.CompanyCount = await companyService.AllCompaniesCountAsync();
            return View(companies);
        }
        #endregion

        #region PremiumCompanies
        public async Task<IActionResult> PremiumCompanies(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = await companyService.PremiumCompaniesPageCountAsync(page);
            ViewBag.CurrentPage = page;

            List<CompanyDto> companies = await companyService.GetPremiumCompaniesWithPagingAsync((int)take, page);
            ViewBag.CompanyCount = await companyService.PremiumCompaniesForAdminPanelCountAsync();
            return View(companies);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            CompanyUpdateDto dbCompany = new CompanyUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                CompanyDescription = user.CompanyDescription,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Address = user.Address,
            };

            return View(dbCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, CompanyUpdateDto company)
        {
            #region Get
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            CompanyUpdateDto dbCompany = new CompanyUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                CompanyDescription = user.CompanyDescription,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Address = user.Address,
            };
            #endregion


            user.Id = company.Id;
            user.Name = company.Name;
            user.CompanyDescription = company.CompanyDescription;
            user.Email = company.Email;
            user.PhoneNumber = company.Phone;
            user.Address = company.Address;

            await userManager.UpdateAsync(user);
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
        public async Task<IActionResult> Activity(int id)
        {
            await companyService.ActivityAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Premium
        public async Task<IActionResult> Premium(int id)
        {
            await companyService.PremiumAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int id)
        //{
        //    await companyService.DeleteAsync(id);
        //    return RedirectToAction("Index");
        //}
        #endregion
    }
}
