using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUnitTestProject.UserServiceTest
{
    public class UserTestBl : IUserBL
    {

        private readonly List<UserDetails> _userDetailst;

        public UserTestBl()
        {
            _userDetailst = new List<UserDetails>()
            {
                new UserDetails() {
                    Id = "user1",
                    Username = "sa1",
                    Password ="sa1",
                    EmailId = "school@gmail.com",
                    UserRoleId =1
                   
                },
                new UserDetails() {
                    Id = "user2",
                    Username = "sa2",
                    Password ="sa2",
                    EmailId = "school@gmail.com",
                    UserRoleId =1

                },
                new UserDetails() {
                    Id = "user3",
                    Username = "sa3",
                    Password ="sa3",
                    EmailId = "school@gmail.com",
                    UserRoleId =1

                },

            };
        }

        public IEnumerable<UserDetails> GetAll()
        {
            return _userDetailst;
        }

        public void InsertUser(UserDetails userDetails)
        {
            userDetails.Id = "user4";
            _userDetailst.Add(userDetails);
        }

        public UserDetails GetById(string id)
        {
            return _userDetailst.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void DeleteUser(string id)
        {
            var existing = _userDetailst.First(a => a.Id == id);
            _userDetailst.Remove(existing);
        }

        public void UpdateUser(string id, UserDetails userDetails)
        {

        }
    }
}
