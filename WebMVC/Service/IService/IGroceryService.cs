using Models;

namespace WebMVC.Service.IService
{
    public interface IGroceryService
    {
        Task<List<Grocery>> GetGroceriesAsync(string? searchString = null);
        Task<Grocery?> GetGroceryByIdAsync(int? id);
        Task<bool> CreateGroceryAsync(Grocery grocery);
        Task<bool> UpdateGroceryAsync(Grocery grocery);
        Task<bool> DeleteGroceryAsync(int id);
    }
}
