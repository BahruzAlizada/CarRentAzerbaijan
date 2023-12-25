using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Helper;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarkaController : Controller
    {
        private readonly IModelService modelService;
        private readonly IWebHostEnvironment env;
        public MarkaController(IModelService modelService,IWebHostEnvironment env)
        {
            this.modelService = modelService;
            this.env = env;
        }

        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            double take = 15;
            ViewBag.PageCount = await modelService.MarkaPageCountAsync(take);
            ViewBag.CurrentPage = page;

            List<Model> models = await modelService.GetAllMarkasAsync((int)take, page);
            return View(models);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(MarkaDto markaDto)
        {
            #region Image
            if (markaDto.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
                return View();
            }
            if (!markaDto.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sadəcə jpeg yaxud jpg tipli fayllar");
                return View();
            }
            if (markaDto.Photo.IsOlder512Kb())
            {
                ModelState.AddModelError("Photo", "Maksimum 512 Kb");
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "images", "markas");
            #endregion

            Model model = new Model
            {
                Name = markaDto.Name,
                Image = await markaDto.Photo.SaveFileAsync(folder),
                IsMain = true,
                IsDeactive = false
            };

            await modelService.AddAsync(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Model dbModel = await modelService.GetMarkaByIdAsync(id);
            if (dbModel == null) return BadRequest();

            MarkaDto dbMarkaDto = new MarkaDto
            {
                Id = dbModel.Id,
                Image = dbModel.Image,
                Name=dbModel.Name,
                IsDeactive = dbModel.IsDeactive
            };

            return View(dbMarkaDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,MarkaDto markaDto)
        {
            #region Get
            if (id == null) return NotFound();
            Model dbModel = await modelService.GetMarkaByIdAsync(id);
            if (dbModel == null) return BadRequest();

            MarkaDto dbMarkaDto = new MarkaDto
            {
                Id = dbModel.Id,
                Image = dbModel.Image,
                Name=dbModel.Name,
                IsDeactive = dbModel.IsDeactive
            };
            #endregion

            Model model = new Model();

            #region Image
            if (markaDto.Photo is not null)
            {
                if (!markaDto.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayllar");
                    return View();
                }
                if (markaDto.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 256Kb");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "images", "markas");
                markaDto.Image = await markaDto.Photo.SaveFileAsync(folder);
                string path = Path.Combine(env.WebRootPath, folder, dbMarkaDto.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                dbMarkaDto.Image = markaDto.Image;
            }
            else
                markaDto.Image = dbMarkaDto.Image;
            #endregion


            model.Id = markaDto.Id;
            model.Name = markaDto.Name;
            model.Image = markaDto.Image;
            model.IsMain = true;
            model.IsDeactive = dbMarkaDto.IsDeactive;

            await modelService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int id)
        {
            await modelService.ActivityAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
