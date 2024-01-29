using CarRentAzerbaijan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentAzerbaijan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion


        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}