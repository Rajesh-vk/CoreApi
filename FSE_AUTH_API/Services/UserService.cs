﻿using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSE_AUTH_API.UserServices
{
    public class UserService : IUserService
    {
        private IConfiguration _config;

        private IUserBL _userBL;

        public UserService(IConfiguration config, IUserBL userBL)
        {
            _config = config;
            _userBL = userBL;
        }

        public string GenerateJSONWebToken(User userInfo)
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
            new User { Username = "admin", Password = "admin", UserRoleId=3 }
        };

        public User AuthenticateUser(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                var DbUser = _userBL.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
                if (DbUser == null)
                    return null;
                else
                    user = new User(DbUser);
            }

            return user;
        }

        public void RegisterUser( UserDetails userDetails)
        {

            _userBL.InsertUser(userDetails);
        }
    }
}
