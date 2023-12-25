using EntityLayer.Concrete;
using System;


namespace BusinessLayer.Abstract
{
    public interface ICityService
    {
        Task<List<City>> GetCityListAsync(int take, int page);
        Task<int> AllCityPageCount(int take);
        Task<List<City>> GetActiveCachingCities();
        Task<List<City>> GetPagedActiveCitiesAsync(string search, int take, int page = 1);
        Task<int> GetCityPageCount(int take);
        Task<List<City>> GetActiveCities();
        Task<City> GetActiveCity(int? id);
        List<City> GetCities();
        City GetCity(int? id);
        void Add(City city);
        void Update(City city);
        Task Activity(int id);
    }
}
