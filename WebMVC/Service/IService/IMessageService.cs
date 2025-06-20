using Models;

namespace WebMVC.Service.IService
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<bool> AddMessageAsync(string roomId, string senderId, string content);
    }
}
