using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
        public async Task<IActionResult> Index(int page=1)
        {
            double take = 20;
            ViewBag.PageCount = await carService.AllCarsPagingCountAsync(take);
            ViewBag.CurrentPage = page;

            List<Car> cars = await carService.GetAllCarsWithPagingAsync((int)take, page);
            ViewBag.CarsCount = await carService.AllCarsCountAsync();
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
    }
}
