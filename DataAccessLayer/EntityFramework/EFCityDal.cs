using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.EntityFramework;

public class EFCityDal : EfRepositoryBase<City, Context>, ICityDal
{
    public async Task Activity(int id)
    {
        using var context = new Context();

        var result = await context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        List<Job> jobs = await context.Jobs.Where(x=>x.CityId==id).ToListAsync();

        if (result.IsDeactive)
        {
            result.IsDeactive = false;
            foreach (var item in jobs)
            {
                item.IsDeactive = false;
            }
        }
        else
        {
            result.IsDeactive = true;
            foreach (var item in jobs)
            {
                item.IsDeactive = true;
            }
        }
            
        await context.SaveChangesAsync();
    }

    public async Task<List<City>> GetActiveCities()
    {
        using var context = new Context();
        List<City> cities = await context.Cities.Where(x => !x.IsDeactive).OrderBy(x => x.Name).ToListAsync();
        return cities;
    }

    public async Task<City> GetActiveCity(int? id)
    {
        using var context = new Context();
        City city = await context.Cities.Where(x => !x.IsDeactive).SingleOrDefaultAsync(x => x.Id == id);
        return city;
    }

    public async Task<int> AllCityPageCount(int take)
    {
        using var context = new Context();

        int pageCount = (int)Math.Ceiling(await context.Cities.CountAsync() / (double)take);
        return pageCount;
    }

    public async Task<List<City>> GetCityListAsync(int take, int page)
    {
        using var context = new Context();

        List<City> cities = await context.Cities.OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).ToListAsync();
        return cities;
    }

    public async Task<int> GetCityPageCount(int take)
    {
        using var context = new Context();

        int pageCount = (int)Math.Ceiling(await context.Cities.Where(x => !x.IsDeactive).CountAsync() / (double)take);
        return pageCount;
    }

    public async Task<List<City>> GetPagedActiveCitiesAsync(string search, int take, int page = 1)
    {
        using var context = new Context();

        List<City> cities = await context.Cities.Where(x => !x.IsDeactive 
        && (search == null || x.Name.Contains(search))).OrderBy(x=>x.Name).Skip((page-1) * take).Take(take)
        .Select(x => new City { Id = x.Id, Name = x.Name }).ToListAsync();

        return cities;
    }
}
