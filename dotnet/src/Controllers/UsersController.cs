using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserServiceLib;
using Entities.Models;
using Microsoft.AspNetCore.Cors;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/jwt")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [EnableCors("MangoPolicy")]
        [HttpPost("authenticate"),Route("login")]
        public IActionResult Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        [EnableCors("MangoPolicy"),Route("users")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}