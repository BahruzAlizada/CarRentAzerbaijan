using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFModelDal : EfRepositoryBase<Model, Context>, IModelDal
    {
        public Task Activity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Model>> GetActiveMarkas()
        {
            using var context = new Context();

            List<Model> markas = await context.Models.Where(x => !x.IsDeactive).ToListAsync();
            return markas;
        }

        public async Task<List<Model>> GetActiveMarkNames()
        {
            using var context = new Context();

            List<Model> markas = await context.Models.Where(x => !x.IsDeactive).Select(x=>new Model
            { Id=x.Id, Name=x.Name}).ToListAsync();
            return markas;
        }
    }
}
