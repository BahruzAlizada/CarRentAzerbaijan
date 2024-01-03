using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal carDal;
        public CarManager(ICarDal carDal)
        {
            this.carDal = carDal;
        }


        public async Task ActivityAsync(int id)
        {
            await carDal.Activity(id);
        }

        public async Task<int> AllCarsCountAsync()
        {
            return await carDal.GetCountAsync();
        }

        public async Task<double> AllCarsPagingCountAsync(double take)
        {
            return await carDal.AllCarsPagingCount(take);
        }

        public async Task<List<Car>> GetAllCarsWithPagingAsync(int take, int page)
        {
            return await carDal.GetAllCarsWithPaging(take, page);
        }
    }
}
