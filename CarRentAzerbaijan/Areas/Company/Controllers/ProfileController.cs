using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles ="Company")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
