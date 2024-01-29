using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class FaqCategoryManager : IFaqCategoryService
    {
        private readonly IFaqCategoryDal faqCategoryDal;
        public FaqCategoryManager(IFaqCategoryDal faqCategoryDal)
        {
            this.faqCategoryDal = faqCategoryDal;
        }


        public void Activity(int id)
        {
            faqCategoryDal.Activity(id);
        }

        public void Add(FaqCategory faqCategory)
        {
            faqCategoryDal.Add(faqCategory);
        }

        public void Delete(int id)
        {
            FaqCategory faqCategory = faqCategoryDal.Get(x=>x.Id == id);
            faqCategoryDal.Delete(faqCategory);
        }

        public List<FaqCategory> GetFaqCategories()
        {
            return faqCategoryDal.GetAll();
        }

        public FaqCategory GetFaqCategory(int? id)
        {
            return faqCategoryDal.Get(x => x.Id == id);
        }

        public void Update(FaqCategory faqCategory)
        {
            faqCategoryDal.Update(faqCategory);
        }

    }
}
