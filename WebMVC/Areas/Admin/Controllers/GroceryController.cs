using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Text;
using WebMVC.Service.IService;
using Microsoft.AspNetCore.Authorization;

namespace WebMVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "Policy1")]
    public class GroceryController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IGroceryService _groceryService;

        public GroceryController(IWebHostEnvironment webHostEnvironment, IGroceryService groceryService)
        {
            _webHostEnviroment = webHostEnvironment;
            _groceryService = groceryService;
        }

        public async Task<IActionResult> Index(string searchString = null)
        {
            List<Grocery> groceries = await _groceryService.GetGroceriesAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                groceries = groceries.Where(g => g.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(groceries);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == 0 || id == null)
            {
                Grocery grocery1 = new Grocery();
                return View(grocery1);
            }
            else
            {
                if (await _groceryService.GetGroceryByIdAsync(id) != null) return View(await _groceryService.GetGroceryByIdAsync(id));
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Grocery grocery, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\grocery");

                    if (!string.IsNullOrEmpty(grocery.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, grocery.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    grocery.ImageUrl = @"\images\grocery\" + fileName;
                }
                else
                {
                    if (grocery.Id == 0)
                    grocery.ImageUrl = @"\images\grocery\b1dcd68c-a4c3-43b0-87c5-b662ed413ee2.png";

                }
                if (grocery.Id == 0)
                {
                    if (await _groceryService.CreateGroceryAsync(grocery)) return RedirectToAction("Index");
                }
                else
                {
                       if (await _groceryService.UpdateGroceryAsync(grocery)) return RedirectToAction("Index");
                }


            }
            return View(grocery);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _groceryService.GetGroceryByIdAsync(id) != null) return View(await _groceryService.GetGroceryByIdAsync(id));
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, string imageurl)
        {
            if (await _groceryService.DeleteGroceryAsync(id))
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, imageurl.TrimStart('\\'));
                if (imageurl != @"\images\grocery\b1dcd68c-a4c3-43b0-87c5-b662ed413ee2.png")
                {
                    System.IO.File.Delete(oldImagePath);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
