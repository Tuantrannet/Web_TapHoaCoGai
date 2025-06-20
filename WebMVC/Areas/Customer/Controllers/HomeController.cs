using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Modles;
using Models;
using Newtonsoft.Json;
using WebMVC.Service.IService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebMVC.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IGroceryService _groceryService;
        public HomeController(/*ILogger<HomeController> logger,*/ IGroceryService groceryService)
        {
            //_logger = logger;
            _groceryService = groceryService;
        }

        public async Task<IActionResult> Index(string searchString, string typeFilter)
        {
            List<Grocery> groceries = await _groceryService.GetGroceriesAsync();

            // L?c
            if (!string.IsNullOrEmpty(searchString))
            {
                groceries = groceries.Where(g => g.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(typeFilter))
            {
                groceries = groceries.Where(g => g.Type == typeFilter).ToList();
            }

            // ??a SelectList vào ViewBag
            var typeList = groceries.Select(g => g.Type).Distinct().ToList();
            ViewBag.TypeList = new SelectList(typeList, typeFilter); // selectedValue = typeFilter

            return View(groceries);
        }
        public async Task<IActionResult> Details(int id)
        {
            Grocery grocery =await _groceryService.GetGroceryByIdAsync(id);
            if (grocery != null)
            {
                return View(grocery);
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
