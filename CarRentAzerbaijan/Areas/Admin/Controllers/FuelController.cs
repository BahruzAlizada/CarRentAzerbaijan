using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FuelController : Controller
    {
        private readonly IFuelService fuelService;
        public FuelController(IFuelService fuelService)
        {
            this.fuelService=fuelService;
        }

        #region Index
        public IActionResult Index()
        {
            List<Fuel> fuels = fuelService.GetAllFuels();
            return View(fuels);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Fuel fuel)
        {
            fuelService.Add(fuel);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Fuel dbFuel = fuelService.GetFuel(id);
            if (dbFuel == null) return BadRequest();

            return View(dbFuel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id,Fuel fuel)
        {
            if (id == null) return NotFound();
            Fuel dbFuel = fuelService.GetFuel(id);
            if (dbFuel == null) return BadRequest();

            dbFuel.Id = fuel.Id;
            dbFuel.Name = fuel.Name;
            dbFuel.IsDeactive = fuel.IsDeactive;

            fuelService.Update(fuel);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            fuelService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
