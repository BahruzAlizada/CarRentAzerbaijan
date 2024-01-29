using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal contactDal;
        public ContactManager(IContactDal contactDal)
        {
            this.contactDal = contactDal;
        }

        public async Task AddAsync(Contact contact)
        {
            await contactDal.AddAsync(contact);
        }

        public double ContactPageCount(double take)
        {
            return contactDal.ContactPageCount(take);
        }

        public void Delete(int id)
        {
            var contact = contactDal.Get(x => x.Id == id);
            contactDal.Delete(contact);
        }

        public Contact GetContact(int? id)
        {
            return contactDal.Get(x => x.Id == id);
        }

        public List<Contact> GetContacts()
        {
            return contactDal.GetAll();
        }

        public List<Contact> GetContactsWithPaged(int take, int page)
        {
            return contactDal.GetContactsWithPaged(take, page);
        }
    }
}
