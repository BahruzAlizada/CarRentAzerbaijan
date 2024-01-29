using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFFaqDal : EfRepositoryBase<FAQ, Context>, IFaqDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            FAQ? fAQ = context.FAQs.FirstOrDefault(x => x.Id == id);
            if (fAQ.IsDeactive)
                fAQ.IsDeactive = false;
            else
                fAQ.IsDeactive = true;

            context.SaveChanges();
        }

        public async Task<List<FAQ>> GetAllFaqsAsyncPaging(int take, int page)
        {
            using var context = new Context();
            
            List<FAQ> faqs = await context.FAQs.Include(x=>x.FaqCategory).OrderByDescending(x=>x.Id).
                Skip((page-1)*take).Take(take).ToListAsync();
            return faqs;
        }

        public async Task<double> GetAllPagingCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.FAQs.CountAsync() / take);
            return pageCount;
        }
    }
}
