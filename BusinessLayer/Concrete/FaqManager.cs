using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class FaqManager : IFaqService
    {
        private readonly IFaqDal faqDal;
        public FaqManager(IFaqDal faqDal)
        {
            this.faqDal = faqDal;    
        }



        public void Activity(int id)
        {
            faqDal.Activity(id);
        }

        public void Add(FAQ faq)
        {
            faqDal.Add(faq);
        }

        public void Delete(int id)
        {
            FAQ faq = faqDal.Get(x => x.Id == id);
            faqDal.Delete(faq);
        }

        public Task<List<FAQ>> GetAllFaqsAsyncPaging(int take, int page)
        {
            return faqDal.GetAllFaqsAsyncPaging(take, page);
        }

        public Task<double> GetAllPagingCountAsync(double take)
        {
            return faqDal.GetAllPagingCountAsync(take);
        }

        public FAQ GetFaqById(int? id)
        {
            return faqDal.Get();
        }

        public void Update(FAQ faq)
        {
            faqDal.Update(faq);
        }
    }
}
