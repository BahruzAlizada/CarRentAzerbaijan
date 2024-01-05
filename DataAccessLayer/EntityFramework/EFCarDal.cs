using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessLayer.EntityFramework
{
    public class EFCarDal : EfRepositoryBase<Car, Context>, ICarDal
    {
        public async Task Activity(int id)
        {
            using var context = new Context();

            Car car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car.IsDeactive)
                car.IsDeactive = false;
            else
                car.IsDeactive = true;

            await context.SaveChangesAsync();
        }

        public async Task<double> AllCarsPagingCount(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Cars.CountAsync() / take);
            return pageCount;
        }

        #region Premium
        public async Task DoPremium(int? id, DateTime time)
        {
            using var context = new Context();

            Car car = await context.Cars.SingleOrDefaultAsync(x => x.Id == id);

            car.IsPremium = true;
            car.PremiumDate = time;

            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }

        public async Task RemovePremium(int? id)
        {
            using var context = new Context();

            Car car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            car.IsPremium = false;

            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }
        #endregion

        public async Task<List<Car>> GetAllCarsWithPaging(int take, int page, FilterDto filter)
        {
            using var context = new Context();

            List<Car> cars = await context.Cars.
                Include(x => x.Ban).Include(x => x.City).
                Include(x => x.Year).Include(x => x.Fuel).
                Include(x => x.GearBox).Include(x => x.CarImages).
                Include(x => x.User).
                Include(x => x.CarModels).ThenInclude(x => x.Model).
                Where(x =>
                (filter.CompanyId == null || x.UserId == filter.CompanyId) &&
                (filter.BanId == null || x.BanId==filter.BanId) &&
                (filter.CityId == null || x.CityId==filter.CityId) &&
                (filter.YearId == null || x.YearId==filter.YearId) &&
                (filter.GearBoxId == null || x.GearBoxId==filter.GearBoxId) &&
                (filter.FuelId == null ||  x.FuelId==filter.FuelId)).
                OrderByDescending(x => x.IsPremium).ThenByDescending(x=>x.Created).
                Skip((page - 1) * take).Take(take).ToListAsync();
            return cars;
        }

        public async Task<List<Car>> GetPremiumCarsWithPaging(int take, int page)
        {
            using var context = new Context();

            List<Car> cars = await context.Cars.
                Include(x => x.Ban).Include(x => x.City).
                Include(x => x.Year).Include(x => x.Fuel).
                Include(x => x.GearBox).Include(x => x.CarImages).
                Include(x => x.User).
                Include(x => x.CarModels).ThenInclude(x => x.Model).
                Where(x=>x.IsPremium).OrderByDescending(x => x.IsPremium).
                Skip((page - 1) * take).Take(take).ToListAsync();
            return cars;
        }

        public async Task<double> PremiumCarsPagingCount(double take)
        {
            using var context = new Context();

            double PageCount = Math.Ceiling(await context.Cars.Where(x => x.IsPremium).CountAsync() / take);
            return PageCount;


        }
    }
}
