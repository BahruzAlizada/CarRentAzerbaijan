using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BusinessLayer.Abstract
{
    public interface ICarService
    {
        Task<List<Car>> ActiveCarsAsync();


        Task ActivityAsync(int id);
        Task DoPremiumAsync(int? id, DateTime time);
        Task RemovePremiumAsync(int? id);
        Task<Car> GetNoIncludeCarAsync(int? id);
        Task<List<Car>> GetAllCarsWithPagingAsync(int take, int page, FilterDto filter);
        Task<double> AllCarsPagingCountAsync(double take);
        Task<int> AllCarsCountAsync();

        Task<Car> GetCarByIdAsync(int? id);

        Task<List<Car>> GetPremiumCarsWithPagingAsync(int take, int page);
        Task<double> PremiumCarsPagingCountAsync(double take);
        Task<int> PremiumCarsCountAsync();


        void Delete(int? id);
    }
}
