using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace JobEntry.Areas.Admin.Controllers
{
    [Area("Admin")]    
    public class CityController : Controller
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            double take = 15;
            ViewBag.PageCount = await cityService.AllCityPageCount((int)take);
            ViewBag.CurrentPage = page;

            List<City> cities = await cityService.GetCityListAsync((int)take,page);
            return View(cities);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(City model)
        {
            #region Exist
            bool isExist = cityService.GetCities().Any(x => x.Name == model.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda şəhər mövcuddur");
                return View();
            }
            #endregion

            cityService.Add(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var dbCity = cityService.GetCity(id);
            if (dbCity == null) return BadRequest();

           

            return View(dbCity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id,City model)
        {
            if (id == null) return NotFound();
            var dbCity = cityService.GetCity(id);
            if (dbCity == null) return BadRequest();

            #region IsExist
            bool isExist = cityService.GetCities().Any(x => x.Name == model.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda şəhər mövcuddur");
                return View();
            }
            #endregion

            dbCity.Id = model.Id;
            dbCity.Name = model.Name;
            dbCity.IsDeactive = model.IsDeactive;
         
            cityService.Update(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int id)
        {
            await cityService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
