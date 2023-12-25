using BusinessLayer.Abstract;
using Core.DataAccess.EntityFramework;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFYearDal : EfRepositoryBase<Year, Context>, IYearDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            Year year = context.Years.FirstOrDefault(x => x.Id == id);
            if (year.IsDeactive)
                year.IsDeactive = false;
            else
                year.IsDeactive = true;

            context.SaveChanges();
        }

        public async Task<List<Year>> GetActiveYearsAsync()
        {
            using var context = new Context();

            List<Year> years = await context.Years.Where(x=>!x.IsDeactive).OrderByDescending(x=>x.Yearr).ToListAsync();
            return years;
        }

        public async Task<List<Year>> GetAllYearsAsync(int take, int page)
        {
            using var context = new Context();

            List<Year> years = await context.Years.OrderByDescending(x => x.Yearr).Skip((page - 1) * take).Take(take).ToListAsync();
            return years;
        }

        public async Task<double> GetYearPageCount(double take)
        {
            var context = new Context();

            double pageCount = Math.Ceiling(await context.Years.CountAsync() / take);
            return pageCount;
        }
    }
}
