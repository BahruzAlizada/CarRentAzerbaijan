using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IModelDal : IRepositoryBase<Model>
    {
        Task Activity(int id);
        Task<List<Model>> GetAllModelsWithPaging(int take, int page);
        Task<double> AllModelPageCount(double take);
        Task<List<Model>> GetAllModelsByParentMarkas(int? parentId);
        Task<List<Model>> GetActiveModelsByParentMarkas(int? parentId);
    }
}
