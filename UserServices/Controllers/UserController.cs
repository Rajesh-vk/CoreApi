using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Mvc;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserBL _userBL;
        public UserController(IUserBL UserBl)
        {
            _userBL = UserBl;

        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserDetails>> Get()
        {
            return _userBL.GetAll().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<UserDetails> Get(string id)
        {
            return _userBL.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] UserDetails userDetails )
        {

            _userBL.InsertUser(userDetails);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] UserDetails userDetails)
        {
            _userBL.UpdateUser(id, userDetails);
        }

        // DELETE api/values/5
        [HttpPost("{id}")]
        public void Delete(string id)
        {
            _userBL.DeleteUser(id);
        }
    }
}
