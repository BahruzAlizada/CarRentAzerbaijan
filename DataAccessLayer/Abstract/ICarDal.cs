using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ICarDal : IRepositoryBase<Car>
    {
        Task Activity(int id);
        Task<List<Car>> GetAllCarsWithPaging(int take,int page);
        Task<double> AllCarsPagingCount(double take);
    }
}
