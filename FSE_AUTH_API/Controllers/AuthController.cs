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
        public IActionResult GetToken([FromBody] UserCredentials login)
        {
            //IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

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

        private string GenerateJSONWebToken(UserCredentials userInfo)
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

        private List<UserCredentials> _users = new List<UserCredentials>
        {
            new UserCredentials { Username = "test", Password = "test" }
        };

        private UserCredentials AuthenticateUser(UserCredentials login)
        {
            var user = _users.SingleOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (user == null)
                return null;
            return user;
        }
    }
}
