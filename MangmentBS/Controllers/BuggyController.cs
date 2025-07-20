using MangmentBS.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly AppDbContext context;

        public BuggyController(AppDbContext _context)
        {
            context = _context;
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequestError()
        {
            return NotFound("This is a not found request.");
        }
        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequestError()
        {
            var result = context.Building.ToList();
            return StatusCode(StatusCodes.Status500InternalServerError, "This is a server error request.");
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequestError()
        {
            return BadRequest("This is a bad request error.");
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized("This is an unauthorized request error.");
        }
        [HttpGet("validationerror")]
        public IActionResult GetValidationError()
        {
            return BadRequest(new { errors = new[] { "This is a validation error." } });
        }
        [HttpGet("test")]
        public IActionResult GetTestError()
        {
            return Ok("This is a test endpoint to check if the API is working correctly.");
        }
    }
}
