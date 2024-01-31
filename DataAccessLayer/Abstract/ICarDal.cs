using Core.DataAccess;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace DataAccessLayer.Abstract
{
    public interface ICarDal : IRepositoryBase<Car>
    {
        Task Activity(int id);
        Task DoPremium(int? id, DateTime time);
        Task RemovePremium(int? id);

        Task<Car> GetCarById(int? id);

        Task<List<Car>> GetAllCarsWithPaging(int take,int page,FilterDto filter );
        Task<double> AllCarsPagingCount(double take);


        Task<List<Car>> GetPremiumCarsWithPaging(int take, int page);
        Task<double> PremiumCarsPagingCount(double take);

        Task<List<Car>> ActiveCarsAsync();
    }
}
