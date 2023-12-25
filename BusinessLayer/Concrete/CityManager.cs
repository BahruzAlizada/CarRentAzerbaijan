using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BusinessLayer.Concrete
{
    public class CityManager : ICityService
    {
        private readonly ICityDal cityDal;
        private readonly IMemoryCache memoryCache;
        public CityManager(ICityDal cityDal,IMemoryCache memoryCache)
        {
            this.cityDal=cityDal;
            this.memoryCache=memoryCache;
        }


        public async Task Activity(int id)
        {
            await cityDal.Activity(id);
        }

        public void Add(City city)
        {
            cityDal.Add(city);
        }

        public List<City> GetCities()
        {
            return cityDal.GetAll();
        }

        public City GetCity(int? id)
        {
            return cityDal.Get(x => x.Id == id);
        }

        public void Update(City city)
        {
            cityDal.Update(city);
        }

        public async Task<List<City>> GetActiveCities()
        {
            return await cityDal.GetActiveCities();
        }

        public async Task<City> GetActiveCity(int? id)
        {
            return await cityDal.GetActiveCity(id);
        }

        public async Task<List<City>> GetPagedActiveCitiesAsync(string search, int take, int page = 1)
        {
            return await cityDal.GetPagedActiveCitiesAsync(search,take,page);
        }

        public async Task<int> GetCityPageCount(int take)
        {
            return await cityDal.GetCityPageCount(take);
        }

        public async Task<List<City>> GetActiveCachingCities()
        {
            const string cacheKey = "cities";
            List<City> cities;

            if(!memoryCache.TryGetValue(cacheKey,out cities))
            {
                cities = await cityDal.GetActiveCities();

                memoryCache.Set(cacheKey, cities, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(4),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(12),
                    Priority = CacheItemPriority.High
                });
            }
            return cities;
        }

        public async Task<List<City>> GetCityListAsync(int take, int page)
        {
            return await cityDal.GetCityListAsync(take,page);
        }

        public async Task<int> AllCityPageCount(int take)
        {
            return await cityDal.AllCityPageCount(take);
        }
    }
}
