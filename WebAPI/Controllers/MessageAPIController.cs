using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/MessageAPI")]
    [ApiController]
    public class MessageAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Message>> GetMessages()
        {
            return Ok(_unitOfWork.Message.GetAll());
        }

        [HttpPost("addmessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AddMessage([FromBody]Message message)
        {
            _unitOfWork.Message.Add(message);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
