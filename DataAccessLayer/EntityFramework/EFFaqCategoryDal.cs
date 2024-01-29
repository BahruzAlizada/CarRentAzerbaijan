using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFFaqCategoryDal : EfRepositoryBase<FaqCategory, Context>, IFaqCategoryDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            FaqCategory? faqCategory = context.FaqCategories.FirstOrDefault(x=>x.Id==id);
            if(faqCategory.IsDeactive)
                faqCategory.IsDeactive=false;
            else
                faqCategory.IsDeactive=true;

            context.SaveChanges();
        }
    }
}
