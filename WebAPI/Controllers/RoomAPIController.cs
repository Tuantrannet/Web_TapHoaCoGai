using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/RoomAPI")]
    [ApiController]
    public class RoomAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Room>> GetRooms()
        {
            return Ok(_unitOfWork.Room.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddRoom([FromBody]Room room)
        {
            if (_unitOfWork.Room.Get(u=> u.Customerid == room.Customerid) != null)
            {
                ModelState.AddModelError("CustomError", "Customerid is already Exist!");
                return BadRequest(ModelState);
            } 

            _unitOfWork.Room.Add(room);
            _unitOfWork.Save();
            return Ok();
        }


    }
}
