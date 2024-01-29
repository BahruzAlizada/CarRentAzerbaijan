using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqController : Controller
    {
        private readonly IFaqService faqService;
        private readonly IFaqCategoryService faqCategoryService;
        public FaqController(IFaqService faqService, IFaqCategoryService faqCategoryService)
        {
            this.faqService = faqService;
            this.faqCategoryService = faqCategoryService;
        }

        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            double take = 20;
            ViewBag.PageCount = await faqService.GetAllPagingCountAsync(take);
            ViewBag.CurrentPage = page;

            List<FAQ> faqs = await faqService.GetAllFaqsAsyncPaging((int)take, page);
            return View(faqs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewBag.FaqCategories = faqCategoryService.GetFaqCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(int catId,FAQ faq)
        {
            ViewBag.FaqCategories = faqCategoryService.GetFaqCategories();

            faq.FaqCategoryId = catId;
            faqService.Add(faq);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            ViewBag.FaqCategories = faqCategoryService.GetFaqCategories();

            if (id == null) return NotFound();
            FAQ dbFaq = faqService.GetFaqById(id);
            if (dbFaq == null) return BadRequest();

            return View(dbFaq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, int catId, FAQ faq)
        {
            ViewBag.FaqCategories = faqCategoryService.GetFaqCategories();

            if (id == null) return NotFound();
            FAQ dbFaq = faqService.GetFaqById(id);
            if (dbFaq == null) return BadRequest();

            dbFaq.Id = faq.Id;
            dbFaq.Quetsion = faq.Quetsion;
            dbFaq.Answer = faq.Answer;
            dbFaq.IsDeactive = faq.IsDeactive;
            faq.FaqCategoryId = catId;

            faqService.Update(faq);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int id)
        {
            faqService.Activity(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            faqService.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
