using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_API_MODEL
{
    public class User
    {

        public User() { }
        public User(UserDetails userDetail)
        {
            Username = userDetail.Username;
            Password = userDetail.Password;
            UserRoleId = userDetail.UserRoleId;
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }

        public int UserRoleId { get; set; }
    }
}
