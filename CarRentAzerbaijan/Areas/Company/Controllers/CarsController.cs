using BusinessLayer.Abstract;
using BusinessLayer.Helper;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentAzerbaijan.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles = "Company")]
    public class CarsController : Controller
    {
        #region Constructor
        private readonly UserManager<AppUser> userManager;
        private readonly IBanService banService;
        private readonly IMarkaService markaService;
        private readonly IModelService modelService;
        private readonly ICityService cityService;
        private readonly IYearService yearService;
        private readonly IFuelService fuelService;
        private readonly IGearBoxService gearBoxService;
        private readonly Context context;
        private readonly IWebHostEnvironment env;
        public CarsController(UserManager<AppUser> userManager, IBanService banService, IMarkaService markaService,
            IModelService modelService, ICityService cityService, IYearService yearService, IFuelService fuelService,
            IGearBoxService gearBoxService,Context context,IWebHostEnvironment env)
        {
            this.userManager = userManager;
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
        public async Task<IActionResult> Index()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            return View();
        }
        #endregion

        #region CreateCar
        public async Task<IActionResult> CreateCar()
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateCar(Car car,int banId,int markaId,int modelId,
            int cityId, int yearId, int fuelId, int gearBoxId)
        {


            #region Get
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.Bans = await banService.GetActiveBansAsync();
            ViewBag.Markas = await markaService.GetActiveCachingMarkNamesAsync();

            ViewBag.Cities = await cityService.GetActiveCachingCities();
            ViewBag.Years = await yearService.GetActiveCachingYearsAsync();
            ViewBag.Fuels = await fuelService.GetActiveFuelsAsync();
            ViewBag.GearBoxs = await gearBoxService.GetActiveGearBoxesAsync();
            #endregion


            #region Image
            List<CarImage> carImages = new List<CarImage>();

            if (car.Photos.Count > 5)
            {
                ModelState.AddModelError("", "Maksimum 5 şəkil yükləməyə icazə verilir.");
                return View();   
            }

            foreach (IFormFile photo in car.Photos)
            {
                if (photo == null)
                {
                    ModelState.AddModelError("photo", "bu xana boş ola bilməz");
                    return View();
                }
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("photo", "Yalnız şəkil tipli fayllar");
                    return View();
                }
                if (photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("photo", "Maksimum 256Kb olmalıdır.");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "images", "cars");

                CarImage carImage = new CarImage
                {
                    Image = await photo.SaveFileAsync(folder)
                };
                carImages.Add(carImage);
            }
            car.CarImages = carImages;
            #endregion

            #region MarkaandModels
            List<CarModels> carModels = new List<CarModels>();

            CarModels CarMarka = new CarModels();
            CarMarka.ModelId = markaId;

            carModels.Add(CarMarka);

            CarModels modelCar = new CarModels();
            modelCar.ModelId = modelId;

            carModels.Add(modelCar);
            #endregion

            car.UserId = user.Id;
            car.BanId = banId;
            car.CityId = cityId;
            car.YearId = yearId;
            car.FuelId = fuelId;
            
            
            if (user.IsPremium)
            {
                car.IsPremium = true;
                car.PremiumDate = (car?.PremiumDate ?? DateTime.UtcNow).AddDays(2);
            }


            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion


        public async Task<IActionResult> LoadModels(int markaId)
        {
            List<Model> models = await modelService.GetActiveModelsByParentMarkasAsync(markaId);
            return PartialView("_LoadModelsPartialView", models);
        }

    }
}
