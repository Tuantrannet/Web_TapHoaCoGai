using DataAccess.Repository.IRepository;
using Models;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Service.IService;

namespace WebMVC.Service
{
    public class GroceryService : IGroceryService
    {
        private readonly HttpClient _httpClient;

        public GroceryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Grocery>> GetGroceriesAsync(string? searchString = null)
        {
            List<Grocery> groceries = new List<Grocery>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7295/api/GroceryAPI");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                groceries = JsonConvert.DeserializeObject<List<Grocery>>(data) ?? new List<Grocery>();
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                groceries = groceries.Where(g => g.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return groceries;
        }

        public async Task<Grocery?> GetGroceryByIdAsync(int? id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7295/api/GroceryAPI/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Grocery>(data);
            }
            return null;
        }

        public async Task<bool> CreateGroceryAsync(Grocery grocery)
        {
            var content = new StringContent(JsonConvert.SerializeObject(grocery), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7295/api/GroceryAPI", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateGroceryAsync(Grocery grocery)
        {
            var content = new StringContent(JsonConvert.SerializeObject(grocery), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7295/api/GroceryAPI/{grocery.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGroceryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7295/api/GroceryAPI/{id}");
            return response.IsSuccessStatusCode;
        }


    }
}
