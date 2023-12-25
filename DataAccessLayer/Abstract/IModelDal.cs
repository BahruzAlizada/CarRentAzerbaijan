
using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IModelDal : IRepositoryBase<Model>
    {
        Task Activity(int id);
        Task<List<Model>> GetActiveMarkas();
        Task<List<Model>> GetActiveMarkNames();
    }
}
