using Azure;
using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFModelDal : EfRepositoryBase<Model, Context>, IModelDal
    {
        public async Task Activity(int id)
        {
            using var context = new Context();

            Model model = await context.Models.FirstOrDefaultAsync(x => x.Id == id);
            if (model.IsDeactive)
                model.IsDeactive = false;
            else
                model.IsDeactive = true;

            await context.SaveChangesAsync();
        }

        public async Task<double> AllModelPageCount(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Models.Where(x => !x.IsMain).CountAsync() / take);
            return pageCount;
        }

        public async Task<List<Model>> GetActiveModelsByParentMarkas(int? parentId)
        {
            using var context = new Context();

            List<Model> models = await context.Models.Where(x =>!x.IsDeactive && !x.IsMain && x.ParentId == parentId).ToListAsync();
            return models;
        }

        public async Task<List<Model>> GetAllModelsByParentMarkas(int? parentId)
        {
            using var context = new Context();

            List<Model> models = await context.Models.Where(x => !x.IsMain && x.ParentId==parentId).ToListAsync();
            return models;
        }

        public async Task<List<Model>> GetAllModelsWithPaging(int take, int page)
        {
            using var context = new Context();

            List<Model> models = await context.Models.Include(x=>x.Parent).Where(x=>!x.IsMain).OrderByDescending(x=>x.Id).
                Skip((page-1)*take).Take(take).ToListAsync();


            return models;
        }
    }
}
