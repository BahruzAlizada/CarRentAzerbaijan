using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFCarDal : EfRepositoryBase<Car, Context>, ICarDal
    {
        public async Task Activity(int id)
        {
            using var context = new Context();

            Car car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car.IsPremium)
                car.IsPremium = false;
            else
                car.IsPremium = true;

            await context.SaveChangesAsync();
        }

        public async Task<double> AllCarsPagingCount(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Cars.CountAsync() / take);
            return pageCount;
        }

        public async Task<List<Car>> GetAllCarsWithPaging(int take, int page)
        {
            using var context = new Context();

            List<Car> cars = await context.Cars.
                Include(x=>x.Ban).
                Include(x => x.City).
                Include(x => x.Year).
                Include(x => x.Fuel).
                Include(x => x.GearBox).
                Include(x=>x.CarImages).
                Include(x=>x.User).
                Include(x=>x.CarModels).ThenInclude(x=>x.Model).
                OrderByDescending(x => x.IsPremium).ThenByDescending(x=>x.Created).
                Skip((page - 1) * take).Take(take).ToListAsync();
            return cars;
        }
    }
}
