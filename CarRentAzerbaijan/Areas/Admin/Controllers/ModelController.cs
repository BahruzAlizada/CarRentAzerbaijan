using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelController : Controller
    {
        private readonly IModelService modelService;
        private readonly IMarkaService markaService;
        public ModelController(IModelService modelService, IMarkaService markaService)
        {
            this.modelService = modelService;
            this.markaService = markaService;
        }

        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            double take = 20;
            ViewBag.PageCount = await modelService.AllModelPageCountAsync(take);
            ViewBag.CurrentPage=page;

            List<Model> models = await modelService.GetAllModelsWithPagingAsync((int)take, page);
            return View(models);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Markas = await markaService.GetActiveMarkNamesAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ModelDto modelDto, int parentId)
        {
            ViewBag.Markas = await markaService.GetActiveMarkNamesAsync();

            Model model = new Model
            {
                Name = modelDto.Name,
                ParentId=parentId,
                IsMain = false,
                IsDeactive = false
            };

            await modelService.AddAsync(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.Markas = await markaService.GetActiveMarkNamesAsync();

            if (id == null) return NotFound();
            Model dbmodel = await modelService.GetModelByIdAsync(id);
            if (dbmodel == null) return BadRequest();

            ModelDto dbModelDto = new ModelDto
            {
                Id = dbmodel.Id,
                Name = dbmodel.Name,
                ParentId = dbmodel.ParentId,
                IsDeactive = dbmodel.IsDeactive,
            };

            return View(dbModelDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,ModelDto modelDto,int parentId)
        {

            ViewBag.Markas = await markaService.GetActiveMarkNamesAsync();

            #region Get
            if (id == null) return NotFound();
            Model dbmodel = await modelService.GetModelByIdAsync(id);
            if (dbmodel == null) return BadRequest();

            ModelDto dbModelDto = new ModelDto
            {
                Id = dbmodel.Id,
                Name = dbmodel.Name,
                ParentId = dbmodel.ParentId,
                IsDeactive = dbmodel.IsDeactive,
            };
            #endregion

            dbModelDto.Id = modelDto.Id;
            dbModelDto.Name = modelDto.Name;
            dbModelDto.IsDeactive = modelDto.IsDeactive;

            Model model = new Model
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                ParentId = parentId,
                IsMain=false,
                IsDeactive = modelDto.IsDeactive,
            };

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
