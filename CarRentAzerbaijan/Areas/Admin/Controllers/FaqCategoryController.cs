using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqCategoryController : Controller
    {
        private readonly IFaqCategoryService faqCategoryService;
        public FaqCategoryController(IFaqCategoryService faqCategoryService)
        {
           this.faqCategoryService = faqCategoryService;
        }

        #region Index
        public IActionResult Index()
        {
            List<FaqCategory> faqCategories = faqCategoryService.GetFaqCategories();
            return View(faqCategories);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(FaqCategory faqCategory)
        {
            faqCategoryService.Add(faqCategory);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            FaqCategory dbFaqCategory = faqCategoryService.GetFaqCategory(id);
            if(dbFaqCategory==null) return BadRequest();

            return View(dbFaqCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, FaqCategory faqCategory)
        {
            if (id == null) return NotFound();
            FaqCategory dbFaqCategory = faqCategoryService.GetFaqCategory(id);
            if (dbFaqCategory == null) return BadRequest();

            dbFaqCategory.Id = faqCategory.Id;
            dbFaqCategory.Name = faqCategory.Name;
            dbFaqCategory.IsDeactive = faqCategory.IsDeactive;

            faqCategoryService.Update(faqCategory);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            faqCategoryService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            faqCategoryService.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
