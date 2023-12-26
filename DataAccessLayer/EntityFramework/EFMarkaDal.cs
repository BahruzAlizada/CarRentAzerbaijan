using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFMarkaDal : EfRepositoryBase<Model, Context>, IMarkaDal
    {
        public async Task Activity(int id)
        {
            using var context = new Context();

            Model model = await context.Models.FirstOrDefaultAsync(x => x.Id == id);
            if (model.IsDeactive)
                model.IsDeactive = false;
            else
                model.IsDeactive= true;

            await context.SaveChangesAsync();
        }

        public async Task<List<Model>> GetActiveMarkas()
        {
            using var context = new Context();

            List<Model> markas = await context.Models.Where(x => !x.IsDeactive && x.IsMain)
                .Select(x=>new Model { Id=x.Id,Name=x.Name,Image=x.Image}).ToListAsync();
            return markas;
        }

        public async Task<List<Model>> GetActiveMarkNames()
        {
            using var context = new Context();

            List<Model> markas = await context.Models.Where(x => !x.IsDeactive && x.IsMain).Select(x=>new Model
            { Id=x.Id, Name=x.Name}).ToListAsync();
            return markas;
        }

        public async Task<List<Model>> GetAllMarkas(int take, int page)
        {
            using var context = new Context();

            List<Model> markas = await context.Models.Where(x=>x.IsMain).OrderByDescending(x=>x.Id)
                .Skip((page-1) * take).Take(take).ToListAsync();
            return markas;
        }

        public async Task<double> MarkaPageCount(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Models.Where(x => x.IsMain).CountAsync() / take);
            return pageCount;
        }
    }
}
