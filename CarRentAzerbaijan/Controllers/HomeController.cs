using BusinessLayer.Abstract;
using CarRentAzerbaijan.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentAzerbaijan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICarService carService;
        public HomeController(ILogger<HomeController> logger,ICarService carService)
        {
            this.logger = logger;
            this.carService = carService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Car> cars = await carService.ActiveCarsAsync();
            return View(cars);
        }
        #endregion


        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}