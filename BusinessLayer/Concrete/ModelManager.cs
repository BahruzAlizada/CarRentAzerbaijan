
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Concrete
{
    public class ModelManager : IModelService
    {
        private readonly IModelDal modelDal;
        private readonly IMemoryCache memoryCache;
        public ModelManager(IModelDal modelDal,IMemoryCache memoryCache)
        {
            this.modelDal = modelDal;
            this.memoryCache = memoryCache;
        }

        public async Task ActivityAsync(int id)
        {
            await modelDal.Activity(id);
        }

        public async Task AddAsync(Model model)
        {
            await modelDal.AddAsync(model);
        }

        public async Task<double> AllModelPageCountAsync(double take)
        {
            return await modelDal.AllModelPageCount(take);
        }

        public async Task<List<Model>> GetActiveCachingModelsByParentMarkasAsync(int? parentId)
        {
            const string cachedKey = "models";
            List<Model> models;

            if(!memoryCache.TryGetValue(cachedKey, out models))
            {
                models = await modelDal.GetActiveModelsByParentMarkas(parentId);

                memoryCache.Set(cachedKey, models, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(3),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(9),
                    Priority=CacheItemPriority.High
                });
            }

            return models;
        }

        public async Task<List<Model>> GetActiveModelsByParentMarkasAsync(int? parentId)
        {
            return await modelDal.GetActiveModelsByParentMarkas(parentId);
        }

        public async Task<List<Model>> GetAllModelsByParentMarkasAsync(int? parentId)
        {
            return await modelDal.GetAllModelsByParentMarkas(parentId);
        }

        public Task<List<Model>> GetAllModelsWithPagingAsync(int take, int page)
        {
            return modelDal.GetAllModelsWithPaging(take, page);
        }

        public async Task<Model> GetModelByIdAsync(int? id)
        {
            return await modelDal.GetAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Model model)
        {
            await modelDal.UpdateAsync(model);
        }
    }
}
