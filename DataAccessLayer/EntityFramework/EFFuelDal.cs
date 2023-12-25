
using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFFuelDal : EfRepositoryBase<Fuel, Context>, IFuelDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            Fuel fuel = context.Fuels.FirstOrDefault(f => f.Id == id);
            if (fuel.IsDeactive)
                fuel.IsDeactive = false;
            else
            fuel.IsDeactive = true;

            context.SaveChanges();
        }

        public async Task<List<Fuel>> GetActiveFuels()
        {
            using var context = new Context();

            List<Fuel> fuels = await context.Fuels.Where(x=>!x.IsDeactive).ToListAsync();
            return fuels;
        }
    }
}
