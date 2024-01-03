using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICarService
    {
        Task ActivityAsync(int id);
        Task<List<Car>> GetAllCarsWithPagingAsync(int take, int page);
        Task<double> AllCarsPagingCountAsync(double take);
        Task<int> AllCarsCountAsync();
    }
}
