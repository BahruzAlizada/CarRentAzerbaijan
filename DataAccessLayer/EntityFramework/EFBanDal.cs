using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFBanDal : EfRepositoryBase<Ban, Context>, IBanDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            Ban ban = context.Bans.FirstOrDefault(b => b.Id == id);
            if (ban.IsDeactive)
                ban.IsDeactive = false;
            else
                ban.IsDeactive = true;

            context.SaveChanges();
        }

        public async Task<List<Ban>> GetActiveBans()
        {
            using var context = new Context();

            List<Ban> bans = await context.Bans.Where(x =>!x.IsDeactive).ToListAsync();
            return bans;
        }
    }
}
