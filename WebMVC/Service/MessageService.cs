using Models;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Service.IService;

namespace WebMVC.Service
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            List<Message> messages = new List<Message>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7295/api/MessageAPI");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                messages = JsonConvert.DeserializeObject<List<Message>>(data) ?? new List<Message>();
            }
            return messages;
        }
        public async Task<bool> AddMessageAsync(string roomId, string senderId, string content) 
        {
            var message = new Message();
            message.Roomid = int.Parse(roomId);
            message.Senderid = int.Parse(senderId);
            message.Content = content;
            var send = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var respone = await _httpClient.PostAsync("https://localhost:7295/api/MessageAPI/addmessage", send);
            return respone.IsSuccessStatusCode;
        }
    }
}
