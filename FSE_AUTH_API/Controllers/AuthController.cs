using FSE_API_MODEL;
using FSE_AUTH_API.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FSE_AUTH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private IUserService _UserService;

        public AuthController(IUserService userService)
        {
            _UserService = userService;
        }


        [HttpPost]
        [Route("GetToken")]
        public IActionResult GetToken([FromBody]User userParam)
        {
            //IActionResult response = Unauthorized();
            var user = _UserService.AuthenticateUser(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);
                //response = Ok(new { token = tokenString, expires = DateTime.Now.AddMinutes(120) });
                user.Token = tokenString;
                user.Password = null;
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public void Post([FromBody] UserDetails userDetails)
        {
            _UserService.RegisterUser(userDetails);
        }


    }
}
