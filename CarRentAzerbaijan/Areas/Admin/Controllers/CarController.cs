using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        #region Index
        public async Task<IActionResult> Index(FilterDto filter,int page=1)
        {
            double take = 20;
            ViewBag.PageCount = await carService.AllCarsPagingCountAsync(take);
            ViewBag.CurrentPage = page;

            List<Car> cars = await carService.GetAllCarsWithPagingAsync((int)take, page, filter);
            ViewBag.CarsCount = await carService.AllCarsCountAsync();
            return View(cars);
        }
        #endregion

        #region PremiumCars
        public async Task<IActionResult> PremiumCars(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = await carService.PremiumCarsPagingCountAsync(take);
            ViewBag.CurrentPage = page;

            List<Car> cars = await carService.GetPremiumCarsWithPagingAsync((int)take, page);
            ViewBag.CarsCount = await carService.PremiumCarsCountAsync();
            return View(cars);
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int id)
        {
            await carService.ActivityAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region DoPremium
        public async Task<IActionResult> DoPremium(int? id) 
        {
            if (id == null) return NotFound();
            Car car = await carService.GetNoIncludeCarAsync(id);
            if (car == null) return BadRequest();

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> DoPremium(int? id, DateTime time)
        {
            if (id == null) return NotFound();
            Car car = await carService.GetNoIncludeCarAsync(id);
            if (car == null) return BadRequest();

            await carService.DoPremiumAsync(id, time);
            return RedirectToAction("Index");
        }
        #endregion

        #region RemovePremium
        public async Task<IActionResult> RemovePremium(int? id)
        {
            if (id == null) return NotFound();
            await carService.RemovePremiumAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if(id==null) return NotFound();
            carService.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
