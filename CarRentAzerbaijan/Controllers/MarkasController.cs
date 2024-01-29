using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Controllers
{
    public class MarkasController : Controller
    {
        private readonly IMarkaService markaService;
        public MarkasController(IMarkaService markaService)
        {
            this.markaService = markaService;     
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Model> markas = await markaService.GetActiveMarkasAsync();
            return View(markas);
        }
        #endregion
    }
}
