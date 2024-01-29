using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

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

        public void Delete(int? id)
        {
            Car car = carDal.Get(x => x.Id == id);
            carDal.Delete(car);
        }

        public async Task DoPremiumAsync(int? id, DateTime time)
        {
            await carDal.DoPremium(id,time);
        }

        public async Task<List<Car>> GetAllCarsWithPagingAsync(int take, int page, FilterDto filter)
        {
            return await carDal.GetAllCarsWithPaging(take, page, filter);
        }

        public async Task<Car> GetCarByIdAsync(int? id)
        {
            return await carDal.GetCarById(id);
        }

        public async Task<Car> GetNoIncludeCarAsync(int? id)
        {
            return await carDal.GetAsync(x => x.Id == id);
        }

        public async Task<List<Car>> GetPremiumCarsWithPagingAsync(int take, int page)
        {
            return await carDal.GetPremiumCarsWithPaging(take, page);
        }

        public async Task<int> PremiumCarsCountAsync()
        {
            return await carDal.GetCountAsync(x => x.IsPremium);
        }

        public async Task<double> PremiumCarsPagingCountAsync(double take)
        {
            return await carDal.PremiumCarsPagingCount(take);
        }

        public async Task RemovePremiumAsync(int? id)
        {
            await carDal.RemovePremium(id);
        }
    }
}
