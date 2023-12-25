using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanController : Controller
    {
        private readonly IBanService banService;
        public BanController(IBanService banService)
        {
            this.banService = banService;
        }

        #region Index
        public IActionResult Index()
        {
            List<Ban> bans = banService.GetAllBans();
            return View(bans);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Ban ban)
        {
            banService.Add(ban);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Ban dbBan = banService.GetBanById(id);
            if (dbBan == null) return BadRequest();

            return View(dbBan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, Ban ban)
        {
            if (id == null) return NotFound();
            Ban dbBan = banService.GetBanById(id);
            if (dbBan == null) return BadRequest();

            dbBan.Id = ban.Id;
            dbBan.Name = ban.Name;
            dbBan.IsDeactive = ban.IsDeactive;

            banService.Update(ban);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            banService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
