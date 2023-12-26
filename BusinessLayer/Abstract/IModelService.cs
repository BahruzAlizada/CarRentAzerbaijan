using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IModelService
    {
        Task ActivityAsync(int id);
        Task<List<Model>> GetAllModelsWithPagingAsync(int take, int page);
        Task<double> AllModelPageCountAsync(double take);
        Task<List<Model>> GetAllModelsByParentMarkasAsync(int? parentId);
        Task<List<Model>> GetActiveModelsByParentMarkasAsync(int? parentId);
        Task<List<Model>> GetActiveCachingModelsByParentMarkasAsync(int? parentId);
        Task<Model> GetModelByIdAsync(int? id);
        Task AddAsync(Model model);
        Task UpdateAsync(Model model);
    }
}
