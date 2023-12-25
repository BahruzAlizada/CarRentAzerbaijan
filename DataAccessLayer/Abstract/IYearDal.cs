using Core.DataAccess;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IYearDal : IRepositoryBase<Year>
    {
        void Activity(int id);
        Task<List<Year>> GetAllYearsAsync(int take, int page);
        Task<List<Year>> GetActiveYearsAsync();
        Task<double> GetYearPageCount(double take);
    }
}
