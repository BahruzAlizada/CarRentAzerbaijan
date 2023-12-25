using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GearBoxController : Controller
    {
        private readonly IGearBoxService gearBoxService;
        public GearBoxController(IGearBoxService gearBoxService)
        {
            this.gearBoxService = gearBoxService;
        }

        #region Index
        public IActionResult Index()
        {
            List<GearBox> gearBoxes = gearBoxService.GetAllGearBoxes();
            return View(gearBoxes);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(GearBox gearBox)
        {
            gearBoxService.Add(gearBox);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            GearBox dbgb = gearBoxService.GetGearBoxById(id);
            if (dbgb == null) return BadRequest();

            return View(dbgb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, GearBox gearBox)
        {
            if (id == null) return NotFound();
            GearBox dbgb = gearBoxService.GetGearBoxById(id);
            if (dbgb == null) return BadRequest();

            dbgb.Id = gearBox.Id;
            dbgb.Name = gearBox.Name;
            dbgb.IsDeactive = gearBox.IsDeactive;

            gearBoxService.Update(gearBox);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            gearBoxService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
