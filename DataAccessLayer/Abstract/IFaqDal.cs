using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IFaqDal : IRepositoryBase<FAQ>
    {
        Task<List<FAQ>> GetAllFaqsAsyncPaging(int take, int page);
        Task<double> GetAllPagingCountAsync(double take);
        void Activity(int id);
    }
}
