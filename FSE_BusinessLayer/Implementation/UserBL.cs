using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using FSE_DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_BusinessLayer.Implementation
{
    public class UserBL : IUserBL
    {
        private IUserRepo userRepo;
        public UserBL(IUserRepo UserRepo)
        {
            userRepo = UserRepo;

        }
        public IEnumerable<UserDetails> GetAll()
        {
            return userRepo.GetAll();

        }

        public UserDetails GetById(string id)
        {
            return userRepo.GetById(id);

        }

        public void InsertUser(UserDetails userDetails)
        {
            var user = userRepo.GetById(userDetails.Id);
            if (user == null)
                userRepo.Insert(userDetails);

        }

        public void UpdateUser(string id, UserDetails userDetails)
        {
            userRepo.Update(id, userDetails);

        }

        public void DeleteUser(string id)
        {
            userRepo.Delete(id);

        }
    }
}
