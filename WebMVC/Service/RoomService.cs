using Models;
using Newtonsoft.Json;
using WebMVC.Service.IService;

namespace WebMVC.Service
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient _httpClient;
        
        public RoomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            List<Room> rooms = new List<Room>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7295/api/RoomAPI");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                rooms = JsonConvert.DeserializeObject<List<Room>>(data) ?? new List<Room>();
            }
            return rooms;
        }
    }
}
