using Models;

namespace WebMVC.Service.IService
{
    public interface ILoginService
    {
        Task<string> GetRole(LoginRequestDTO loginRequestDTO);
        Task<bool> IsValid(LoginRequestDTO loginRequestDTO);
        Task<bool> AddUser(RegisterRequestDTO registerRequestDTO);
        Task<List<User>> GetUsersAsync();

        Task<int> GetIdAsync(LoginRequestDTO registerRequestDTO);
    }
}
