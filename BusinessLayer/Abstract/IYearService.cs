using Core.DataAccess;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IYearService
    {
        Task<List<Year>> GetAllYearsAsync(int take,int page);
        Task<List<Year>> GetActiveYearsAsync();
        Task<List<Year>> GetActiveCachingYearsAsync();
        Year GetYearById(int? id);
        Task<double> GetYearPageCount(double take);
        void Add(Year year);
        void Update(Year year);
        void Delete(int id);
        void Activity(int id);

    }
}
