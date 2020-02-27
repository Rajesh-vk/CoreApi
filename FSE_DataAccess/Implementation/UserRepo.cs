using FSE_API_MODEL;
using FSE_DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_DataAccess.Implementation
{
    public class UserRepo : CosmosRepositoryBase<UserDetails>, IUserRepo
    {
        public UserRepo() : base("UserCosmosDocCollection") {
        }
    }
}
