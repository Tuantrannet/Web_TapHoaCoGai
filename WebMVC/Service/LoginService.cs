using Azure;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebMVC.Service.IService;

namespace WebMVC.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> users = new List<User>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7295/api/LoginAPI");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();
            }
            return users;
        }

        public async Task<int> GetIdAsync(LoginRequestDTO loginRequestDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginRequestDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage respone = await _httpClient.PostAsync($"https://localhost:7295/api/LoginAPI/login", content);
            if (respone.IsSuccessStatusCode)
            {
                var data = await respone.Content.ReadAsStringAsync();
                LoginResponseDTO loginresponedto = JsonConvert.DeserializeObject<LoginResponseDTO>(data);
                return loginresponedto.Id;
            }
            return 0;
        }

        public async Task<String> GetRole(LoginRequestDTO loginRequestDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginRequestDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage respone = await _httpClient.PostAsync($"https://localhost:7295/api/LoginAPI/login", content);
            if (respone.IsSuccessStatusCode)
            {
                var data = await respone.Content.ReadAsStringAsync();
                LoginResponseDTO loginresponedto = JsonConvert.DeserializeObject<LoginResponseDTO>(data);
                return loginresponedto.Role;
            }
            return null;
        }

        public async Task<bool> IsValid(LoginRequestDTO loginRequestDTO)
        {
            bool isValid = false;
            var content = new StringContent(JsonConvert.SerializeObject(loginRequestDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage respone = await _httpClient.PostAsync($"https://localhost:7295/api/LoginAPI/login", content);
            if (respone.IsSuccessStatusCode)
            {
                var data = await respone.Content.ReadAsStringAsync();
                LoginResponseDTO loginresponedto = JsonConvert.DeserializeObject<LoginResponseDTO>(data);
                if (loginresponedto.Success) { isValid = true; }
            }
            return isValid;
        }

        public async Task<bool> AddUser(RegisterRequestDTO registerRequestDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(registerRequestDTO), Encoding.UTF8, "application/json");
            var respone = await _httpClient.PostAsync("https://localhost:7295/api/LoginAPI/register", content);
            return respone.IsSuccessStatusCode;
        }
    }
}
