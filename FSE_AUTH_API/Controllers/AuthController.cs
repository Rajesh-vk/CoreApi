using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FSE_AUTH_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FSE_AUTH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost]
        [Route("GetToken")]
        public IActionResult GetToken([FromBody]User userParam)
        {
            //IActionResult response = Unauthorized();
            var user = AuthenticateUser(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                //response = Ok(new { token = tokenString, expires = DateTime.Now.AddMinutes(120) });
                user.Token = tokenString;
                user.Password = null;
            }

            return Ok(user);
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<User> _users = new List<User>
        {
            new User { Username = "test", Password = "test" }
        };

        private User AuthenticateUser(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;
            return _users.FirstOrDefault();
        }
    }
}
