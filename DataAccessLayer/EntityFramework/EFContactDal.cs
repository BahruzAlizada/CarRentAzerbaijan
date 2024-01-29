
using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFContactDal : EfRepositoryBase<Contact,Context>,IContactDal
    {
        public double ContactPageCount(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(context.Contacts.Count() / take);
            return pageCount;
        }

        public List<Contact> GetContactsWithPaged(int take, int page)
        {
            using var context = new Context();

            List<Contact> contacts = context.Contacts.OrderByDescending(x => x.Id).
                Skip((page - 1) * take).Take(take).ToList();
            return contacts;
        }
    }
}
