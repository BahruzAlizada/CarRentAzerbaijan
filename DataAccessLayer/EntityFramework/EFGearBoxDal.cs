using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFGearBoxDal : EfRepositoryBase<GearBox, Context>, IGearBoxDal
    {
        public void Activity(int id)
        {
            using var context = new Context();

            GearBox gearBox = context.GearBoxes.FirstOrDefault(x => x.Id == id);
            if (gearBox.IsDeactive)
                gearBox.IsDeactive = false;
            else
                gearBox.IsDeactive=true;

            context.SaveChanges();
        }

        public async Task<List<GearBox>> GetActiveGearBoxes()
        {
            using var context = new Context();

            List<GearBox> gearBoxes = await context.GearBoxes.Where(x => !x.IsDeactive).ToListAsync(); 
            return gearBoxes;
        }
    }
}
