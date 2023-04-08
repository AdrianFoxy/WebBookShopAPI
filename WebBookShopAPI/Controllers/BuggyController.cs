using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data;
using WebBookShopAPI.Data.Errors;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BuggyController(AppDbContext context) 
        { 
            _context = context;
        }


        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("testauth_admin")]
        [Authorize(Roles = "Admin")]     
        public ActionResult<string> GetAdminSecretText()
        {
            return "admin secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() 
        {
            var thing = _context.Book.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Book.Find(42);

            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badreguest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
