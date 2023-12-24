using Core.DataAccess;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using System;


namespace DataAccessLayer.Abstract
{
    public interface ICityDal : IRepositoryBase<City>
    {
        Task<List<City>> GetCityListAsync(int take, int page);
        Task<int> AllCityPageCount(int take);
        Task<List<City>> GetPagedActiveCitiesAsync(string search, int take, int page=1);
        Task<int> GetCityPageCount(int take); 
        Task<List<City>> GetActiveCities();
        Task<City> GetActiveCity(int? id);
        Task Activity(int id);
    }
}
