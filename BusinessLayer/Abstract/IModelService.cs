
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IModelService
    {
        Task<List<Model>> GetAllMarkasAsync();
        Task<List<Model>> GetActiveMarkasAsync();
        Task<List<Model>> GetActiveMarkNamesAsync();
        Task<List<Model>> GetActiveCachingMarkNamesAsync();
        Task<Model> GetMarkaById(int? id);
        Task AddAsync(Model marka);
        Task UpdateAsync(Model marka);
        Task ActivityAsync(int id);
    }
}
