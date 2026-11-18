using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new
            {
                message = "Anyone can access this endpoint."
            });
        }

        [HttpGet("student")]
        [Authorize(Roles = "Student")]
        public IActionResult Student()
        {
            return Ok(new
            {
                message = "Welcome Student! You have access to this endpoint."
            });
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return Ok(new
            {
                message = "Welcome Admin! You have access to this endpoint."
            });
        }
    }
}