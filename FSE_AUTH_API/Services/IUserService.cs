using FSE_API_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE_AUTH_API.UserServices
{
    public interface IUserService
    {
        string GenerateJSONWebToken(User userInfo);
        User AuthenticateUser(string username, string password);
    }
}
