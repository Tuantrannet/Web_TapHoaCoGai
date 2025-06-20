using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using WebMVC.Service.IService;

namespace WebMVC.Areas.Common.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRoomService _roomService;
        private readonly IMessageService _messageService;

        public ChatController(ILoginService loginService, IRoomService roomService, IMessageService messageService)
        {
            _loginService = loginService;
            _roomService = roomService;
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = await _loginService.GetUsersAsync();
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            List<User> finalusers = new List<User>();
            if (role == "Admin")
            {
                finalusers = users.Where(u => u.Roleid == 2).ToList();
                return View(finalusers);
            }
            finalusers = users.Where(u => u.Roleid == 1).ToList();

            return View(finalusers);
        }

        public async Task<IActionResult> Room(int customerId)
        {
            var room = await _roomService.GetRoomsAsync();
            var filterroom = room.FirstOrDefault(u => u.Customerid == customerId);

            List<Message> messages = await _messageService.GetMessagesAsync();
            var filtermessages = messages.Where(m => m.Roomid == filterroom.Id).OrderBy(m=> m.Id).ToList();

            ViewBag.RoomId = filterroom.Id.ToString();
            ViewBag.CustomerId = customerId;

            return View(messages);

        }
    }
}
