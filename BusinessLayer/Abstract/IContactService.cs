

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IContactService
    {
        List<Contact> GetContactsWithPaged(int take, int page);
        double ContactPageCount(double take);
        List<Contact> GetContacts();
        Contact GetContact(int? id);
        Task AddAsync(Contact contact);
        void Delete(int id);
    }
}
