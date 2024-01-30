using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SocialMediaController : Controller
	{
		private readonly ISocialMediaService socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
			this.socialMediaService = socialMediaService;
        }

		#region Index
		public IActionResult Index()
		{
			List<SocialMedia> socialMedia = socialMediaService.GetSocialMedias();
			return View(socialMedia);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Create(SocialMedia socialMedia)
		{
			socialMediaService.Add(socialMedia);
			return RedirectToAction("Index");
		}
		#endregion

		#region Update
		public IActionResult Update(int? id)
		{
			if (id == null) return NotFound();
			SocialMedia dbSocialMedia = socialMediaService.GetSocialMediaByID(id);
			if (dbSocialMedia == null) return BadRequest();


			return View(dbSocialMedia);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Update(int? id, SocialMedia socialMedia)
		{
			if (id == null) return NotFound();
			SocialMedia dbSocialMedia = socialMediaService.GetSocialMediaByID(id);
			if (dbSocialMedia == null) return BadRequest();


			dbSocialMedia.Id = socialMedia.Id;
			dbSocialMedia.Icon = socialMedia.Icon;
			dbSocialMedia.IsDeactive = socialMedia.IsDeactive;
			dbSocialMedia.Link = socialMedia.Link;

			socialMediaService.Update(socialMedia);
			return RedirectToAction("Index");
		}
		#endregion

		#region Activity
		public IActionResult Activity(int id)
		{
			socialMediaService.Activity(id);
			return RedirectToAction("Index");
		}
		#endregion

		#region Delete
		public IActionResult Delete(int id)
		{
			socialMediaService.Delete(id);
			return RedirectToAction("Index");
		}
		#endregion
	}
}
