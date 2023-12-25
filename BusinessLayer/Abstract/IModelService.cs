
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IModelService
    {
        Task<List<Model>> GetActiveMarkasAsync();
        Task<List<Model>> GetActiveMarkNamesAsync();
        Task<List<Model>> GetActiveCachingMarkNamesAsync();

        Task<List<Model>> GetAllMarkasAsync(int take, int page);
        Task<double> MarkaPageCountAsync(double take);

        Task<Model> GetMarkaByIdAsync(int? id);
        Task AddAsync(Model marka);
        Task UpdateAsync(Model marka);
        Task ActivityAsync(int id);
    }
}
