using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarRentAzerbaijan.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService cityService;
        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService; 
        }

        #region Index
        public async Task<IActionResult> Index(string search,int page = 1)
        {
            int take = 21;
            ViewBag.PageCount = await cityService.GetCityPageCount(take);
            ViewBag.CurrentPage = page;

            List<City> cities = await cityService.GetPagedActiveCitiesAsync(search, take, page);
            return View(cities);
        }
        #endregion
    }
}
