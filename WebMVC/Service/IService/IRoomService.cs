using Models;

namespace WebMVC.Service.IService
{
    public interface IRoomService
    {
        Task<List<Room>> GetRoomsAsync();
    }
}
