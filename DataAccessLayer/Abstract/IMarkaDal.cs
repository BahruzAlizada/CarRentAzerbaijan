using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IMarkaDal : IRepositoryBase<Model>
    {
        Task Activity(int id);
        Task<List<Model>> GetAllMarkas(int take,int page);
        Task<List<Model>> GetActiveMarkas();
        Task<List<Model>> GetActiveMarkNames();
        Task<double> MarkaPageCount(double take);
    }
}
