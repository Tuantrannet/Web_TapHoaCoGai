using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/GroceryAPI")]
    [ApiController]

    public class GroceryAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroceryAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Grocery>> GetGroceries()
        {
            return Ok(_unitOfWork.Grocery.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetGrocery")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Grocery> GetGrocery(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    
            var grocery = _unitOfWork.Grocery.Get(u => u.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }    
            return Ok(grocery);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Grocery> CreateGrocery([FromBody]Grocery grocery)
        {
            if (_unitOfWork.Grocery.Get(u => u.Name.ToLower() == grocery.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Grocery is already Exist!");
                return BadRequest(ModelState);
            }    
            if (grocery == null)
            {
                return BadRequest();
            }    
            if (grocery.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }    
            _unitOfWork.Grocery.Add(grocery);
            _unitOfWork.Save();
            return CreatedAtRoute("GetGrocery", new { id = grocery.Id }, grocery);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteGrocery(int id)
        {
            if (id <= 0 )
            {
                return BadRequest();
            }
            var grocery = _unitOfWork.Grocery.Get(u => u.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }    
            _unitOfWork.Grocery.Remove(grocery);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateGrocery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateGrocery(int id,  Grocery grocery)
        {
            if (grocery == null || grocery.Id != id)
            {
                return BadRequest();
            }    
            _unitOfWork.Grocery.Update(grocery);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialGrocery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePartialGrocery(int id, JsonPatchDocument<Grocery> patch)
        {
            if (patch == null || id == 0)
            {
                return BadRequest();
            }    
            var grocery = _unitOfWork.Grocery.Get(u => u.Id == id);
            if (grocery == null)
            {
                return BadRequest();
            }
            patch.ApplyTo(grocery, ModelState);
            _unitOfWork.Grocery.Update(grocery);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
