using BusinessLayer.Abstract;
using BusinessLayer.Helper;
using CarRentAzerbaijan.ViewModels.WebUserVM;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        #region Constructor
        private readonly UserManager<AppUser> userManager;
        private readonly ICarService carService;
        private readonly IBanService banService;
        private readonly IMarkaService markaService;
        private readonly IModelService modelService;
        private readonly ICityService cityService;
        private readonly IYearService yearService;
        private readonly IFuelService fuelService;
        private readonly IGearBoxService gearBoxService;
        private readonly Context context;
        private readonly IWebHostEnvironment env;
        public CarController(UserManager<AppUser> userManager,ICarService carService, IBanService banService, IMarkaService markaService,
            IModelService modelService, ICityService cityService, IYearService yearService, IFuelService fuelService,
            IGearBoxService gearBoxService, Context context, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.carService = carService;
            this.banService = banService;
            this.markaService = markaService;
            this.modelService = modelService;
            this.cityService = cityService;
            this.yearService = yearService;
            this.fuelService = fuelService;
            this.gearBoxService = gearBoxService;
            this.context = context;
            this.env = env;
        }
        #endregion

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

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.Bans = await banService.GetActiveBansAsync();
            ViewBag.Markas = await markaService.GetActiveCachingMarkNamesAsync();
            ViewBag.Models = await modelService.GetActiveModelsByParentMarkasAsync(1);
            ViewBag.Cities = await cityService.GetActiveCachingCities();
            ViewBag.Years = await yearService.GetActiveCachingYearsAsync();
            ViewBag.Fuels = await fuelService.GetActiveFuelsAsync();
            ViewBag.GearBoxs = await gearBoxService.GetActiveGearBoxesAsync();

            if (id == null) return NotFound();
            Car dbCar = await carService.GetCarByIdAsync(id);
            if (dbCar == null) return BadRequest();

            return View(dbCar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Car car, int banId, int markaId, int modelId,
            int cityId, int yearId, int fuelId, int gearBoxId)
        {
            #region Get
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.Bans = await banService.GetActiveBansAsync();
            ViewBag.Markas = await markaService.GetActiveCachingMarkNamesAsync();
            ViewBag.Models = await modelService.GetActiveModelsByParentMarkasAsync(1);
            ViewBag.Cities = await cityService.GetActiveCachingCities();
            ViewBag.Years = await yearService.GetActiveCachingYearsAsync();
            ViewBag.Fuels = await fuelService.GetActiveFuelsAsync();
            ViewBag.GearBoxs = await gearBoxService.GetActiveGearBoxesAsync();

            if (id == null) return NotFound();
            Car dbCar = await carService.GetCarByIdAsync(id);
            if (dbCar == null) return BadRequest();
            #endregion

            #region Outside Image
            if (car.OutsidePhoto is not null)
            {
                if (!car.OutsidePhoto.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayllar");
                    return View();
                }
                if (car.OutsidePhoto.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 256Kb");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "images", "cars");
                car.OutsideImage = await car.OutsidePhoto.SaveFileAsync(folder);
                string path = Path.Combine(env.WebRootPath, folder, dbCar.OutsideImage);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                dbCar.OutsideImage = car.OutsideImage;
            }
            else
                car.OutsideImage = dbCar.OutsideImage;
            #endregion

            #region Inside Image
            if (car.InsidePhoto is not null)
            {
                if (!car.InsidePhoto.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayllar");
                    return View();
                }
                if (car.InsidePhoto.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 256Kb");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "images", "cars");
                car.InsideImage = await car.InsidePhoto.SaveFileAsync(folder);
                string path = Path.Combine(env.WebRootPath, folder, dbCar.InsideImage);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                dbCar.InsideImage = car.InsideImage;
            }
            else
                car.InsideImage = dbCar.InsideImage;
            #endregion

            dbCar.Id = car.Id;
            dbCar.Description = car.Description;
            dbCar.DailyPrice = car.DailyPrice;
            dbCar.IsFull = car.IsFull;
            dbCar.IsDeactive = car.IsDeactive;
            dbCar.IsPremium = car.IsPremium;
            dbCar.Created = car.Created;
            dbCar.PremiumDate=car.PremiumDate;

            car.UserId = user.Id;
            car.BanId = banId;
            car.CityId = cityId;
            car.YearId = yearId;
            car.FuelId = fuelId;
            car.GearBoxId= gearBoxId;

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
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
