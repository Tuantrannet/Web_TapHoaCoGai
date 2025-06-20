using Microsoft.AspNetCore.SignalR;
using WebMVC.Service.IService;
namespace WebMVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // Thêm client vào group tương ứng với room
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        // Gửi tin nhắn tới tất cả các client trong group
        public async Task SendMessageToRoom(string roomId, string senderId, string content)
        {
            if (await _messageService.AddMessageAsync(roomId, senderId, content))
            {
                await Clients.Group(roomId).SendAsync("ReceiveMessage", senderId, content);
            }
            else
            {
                Console.WriteLine("❌ Failed to save message to DB");
            }

        }
    }
}
