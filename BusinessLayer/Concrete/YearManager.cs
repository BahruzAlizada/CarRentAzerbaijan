using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Concrete
{
    public class YearManager : IYearService
    {
        private readonly IYearDal yearDal;
        private readonly IMemoryCache memoryCache;
        public YearManager(IYearDal yearDal,IMemoryCache memoryCache)
        {
            this.yearDal = yearDal;
            this.memoryCache = memoryCache;
        }


        public void Activity(int id)
        {
            yearDal.Activity(id);
        }

        public void Add(Year year)
        {
            yearDal.Add(year);
        }

        public void Delete(int id)
        {
            Year year = yearDal.Get(x=>x.Id==id);
            yearDal.Delete(year);
        }

        public async Task<List<Year>> GetActiveCachingYearsAsync()
        {
            const string cachedKey = "years";
            List<Year> years;

            if(!memoryCache.TryGetValue(cachedKey, out years))
            {
                years = await yearDal.GetActiveYearsAsync();

                memoryCache.Set(cachedKey, years,new MemoryCacheEntryOptions
                {
                    SlidingExpiration=TimeSpan.FromMinutes(4),
                    AbsoluteExpirationRelativeToNow=TimeSpan.FromMinutes(12)
                });
            }

            return years;
        }

        public async Task<List<Year>> GetActiveYearsAsync()
        {
            return await yearDal.GetActiveYearsAsync();
        }

        public async Task<List<Year>> GetAllYearsAsync(int take, int page)
        {
            return await yearDal.GetAllYearsAsync(take, page);
        }

        public Year GetYearById(int? id)
        {
            return yearDal.Get(x => x.Id == id);
        }

        public async Task<double> GetYearPageCount(double take)
        {
            return await yearDal.GetYearPageCount(take);
        }

        public void Update(Year year)
        {
            yearDal.Update(year);
        }
    }
}
