using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YearController : Controller
    {
        private readonly IYearService yearService;
        public YearController(IYearService yearService)
        {
            this.yearService = yearService;
        }

        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            double take = 15;
            ViewBag.PageCount = await yearService.GetYearPageCount(take);
            ViewBag.CurrentPage = page;

            List<Year> years = await yearService.GetAllYearsAsync((int)take, page);
            return View(years);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Year year)
        {
            yearService.Add(year);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Year dbYear = yearService.GetYearById(id);
            if (dbYear == null) return BadRequest();

            return View(dbYear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, Year year)
        {
            if (id == null) return NotFound();
            Year dbYear = yearService.GetYearById(id);
            if (dbYear == null) return BadRequest();

            dbYear.Id = year.Id;
            dbYear.Yearr = year.Yearr;
            dbYear.IsDeactive = year.IsDeactive;

            yearService.Update(year);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            yearService.Activity(id); 
            return RedirectToAction("Index");
        }
        #endregion
    }
}
