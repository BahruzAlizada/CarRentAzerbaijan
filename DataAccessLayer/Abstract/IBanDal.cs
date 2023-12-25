using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IBanDal : IRepositoryBase<Ban>
    {
        void Activity(int id);
        Task<List<Ban>> GetActiveBans();
    }
}
