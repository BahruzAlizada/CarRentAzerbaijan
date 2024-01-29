

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IFaqCategoryService
    {
        List<FaqCategory> GetFaqCategories();
        FaqCategory GetFaqCategory(int? id);

        void Add(FaqCategory faqCategory);
        void Update(FaqCategory faqCategory);
        void Delete(int id);
        void Activity(int id);
    }
}
