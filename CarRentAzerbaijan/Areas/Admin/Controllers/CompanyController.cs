using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
