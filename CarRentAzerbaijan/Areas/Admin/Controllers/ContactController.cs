using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        #region Index
        public IActionResult Index(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = contactService.ContactPageCount(take);
            ViewBag.CurrentPage = page;

            List<Contact> contacts = contactService.GetContactsWithPaged((int)take, page);
            return View(contacts);
        }
        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = contactService.GetContact(id);
            if (contact == null) return BadRequest();
            return View(contact);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            contactService.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
