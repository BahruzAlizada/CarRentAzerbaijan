using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Concrete
{
    public class ModelManager : IModelService
    {
        private readonly IModelDal markaDal;
        private readonly IMemoryCache memoryCache;
        public ModelManager(IModelDal markaDal,IMemoryCache memoryCache)
        {
            this.markaDal = markaDal;
            this.memoryCache= memoryCache;
        }

        public async Task ActivityAsync(int id)
        {
            await markaDal.Activity(id);
        }

        public async Task AddAsync(Model marka)
        {
            await markaDal.AddAsync(marka);
        }

        public async Task<List<Model>> GetActiveCachingMarkNamesAsync()
        {
            const string cachedKey = "markas";
            List<Model> markas;

            if(!memoryCache.TryGetValue(cachedKey, out markas))
            {
                markas = await markaDal.GetActiveMarkNames();

                memoryCache.Set(cachedKey, markas, new MemoryCacheEntryOptions
                {
                    SlidingExpiration=TimeSpan.FromMinutes(4),
                    AbsoluteExpirationRelativeToNow=TimeSpan.FromMinutes(12),
                    Priority=CacheItemPriority.High
                });
            }

            return markas;
        }

        public async Task<List<Model>> GetActiveMarkasAsync()
        {
            return await markaDal.GetActiveMarkas();
        }

        public async Task<List<Model>> GetActiveMarkNamesAsync()
        {
            return await markaDal.GetActiveMarkNames();
        }

        public async Task<List<Model>> GetAllMarkasAsync(int take, int page)
        {
            return await markaDal.GetAllMarkas(take, page);
        }

        public async Task<Model> GetMarkaByIdAsync(int? id)
        {
            return await markaDal.GetAsync(x => x.Id == id);
        }

        public async Task<double> MarkaPageCountAsync(double take)
        {
            return await markaDal.MarkaPageCount(take);
        }

        public async Task UpdateAsync(Model marka)
        {
            await markaDal.UpdateAsync(marka);
        }
    }
}
