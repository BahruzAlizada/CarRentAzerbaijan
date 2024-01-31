using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AboutController : Controller
	{
		private readonly IAboutService aboutService;
        public AboutController(IAboutService aboutService)
        {
			this.aboutService = aboutService;
        }

		#region Index
		public IActionResult Index()
		{
			About about = aboutService.GetAbout();
			return View(about);
		}
		#endregion

		#region Update
		public IActionResult Update(int? id)
		{
			if (id == null) return NotFound();
			About dbAbout = aboutService.GetAboutByID(id);
			if (dbAbout == null) return BadRequest();

			return View(dbAbout);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Update(int? id,About about)
		{
			if (id == null) return NotFound();
			About dbAbout = aboutService.GetAboutByID(id);
			if (dbAbout == null) return BadRequest();

			dbAbout.Id = about.Id;
			dbAbout.Description = about.Description;

			aboutService.Update(dbAbout);
			return RedirectToAction("Index");
		}

		#endregion

	}
}
