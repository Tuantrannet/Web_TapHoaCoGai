using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/LoginAPI")]
    [ApiController]
    public class LoginAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_unitOfWork.User.GetAll());
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginDto)
        {
            var user = _unitOfWork.User.Get(u => u.Username == loginDto.Username && u.Password == loginDto.Password, "Role");

            if (user == null)
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Id = 0,
                    Success = false,
                    Message = "Sai tài khoản hoặc mật khẩu"
                });
            }

            return Ok(new LoginResponseDTO
            {
                Id = user.Id,
                Success = true,
                Role = user.Role.Name
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Register([FromBody] RegisterRequestDTO registerDto)
        {
            if (_unitOfWork.User.Get(u=> u.Username.ToLower() ==  registerDto.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Username is already Exist!");
                return BadRequest(ModelState);
            }
            var user = new User();
            user.Username = registerDto.Username;
            user.Password = registerDto.Password;
            user.Lastlogin = DateTime.Now;
            user.Roleid = 2;
            _unitOfWork.User.Add(user);

            _unitOfWork.Save();

            var room = new Room();
            room.Customerid = user.Id;
            _unitOfWork.Room.Add(room);

            _unitOfWork.Save();
            return Ok();

        }

        

    }
}
