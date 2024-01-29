
using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IContactDal : IRepositoryBase<Contact>
    {
        List<Contact> GetContactsWithPaged(int take, int page);
        double ContactPageCount(double take);
    }
}
