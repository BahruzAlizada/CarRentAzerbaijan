using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IFuelDal : IRepositoryBase<Fuel>
    {
        void Activity(int id);
        Task<List<Fuel>> GetActiveFuels();
    }
}
