using EntityLayer.Concrete;
using EntityLayer.Dto;
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
        void Add(CityDto cityDto);
        void Update(CityDto cityDto);
        Task Activity(int id);
    }
}
