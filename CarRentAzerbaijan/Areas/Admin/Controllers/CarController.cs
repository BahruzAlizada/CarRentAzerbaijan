using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Areas.Admin.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
