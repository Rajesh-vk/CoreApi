using FSE_API_MODEL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_BusinessLayer.Inferface
{
    public interface IUserBL
    {
        IEnumerable<UserDetails> GetAll();
        UserDetails GetById(string id);
        void InsertUser(UserDetails userDetails);
        void UpdateUser(string id, UserDetails userDetails);
        void DeleteUser(string id);
    }
}
