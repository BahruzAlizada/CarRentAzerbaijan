

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IFaqService
    {
        Task<List<FAQ>> GetAllFaqsAsyncPaging(int take, int page);
        Task<double> GetAllPagingCountAsync(double take);
        FAQ GetFaqById(int? id);

        void Add(FAQ faq);
        void Update(FAQ faq);
        void Delete(int id);
        void Activity(int id);
    }
}
