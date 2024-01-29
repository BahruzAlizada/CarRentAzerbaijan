using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IFaqCategoryDal : IRepositoryBase<FaqCategory>
    {
        void Activity(int id);
    }
}
