using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            return Ok(_userBL.GetAll().ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<UserDetails> Get(string id)
        {
            var item = _userBL.GetById(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
          
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] UserDetails userDetails )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userBL.InsertUser(userDetails);

            return CreatedAtAction("Get", new { id = userDetails.Id }, userDetails);
           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] UserDetails userDetails)
        {
            _userBL.UpdateUser(id, userDetails);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingItem = _userBL.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            _userBL.DeleteUser(id);

            return Ok();
        }
    }
}
