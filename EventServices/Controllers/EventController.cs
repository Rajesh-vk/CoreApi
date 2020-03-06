using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {

        private IEvent _eventBL;


        public EventController(IEvent eventBl)
        {
            _eventBL = eventBl;

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<EventDetails>> Get()
        {
            return Ok(_eventBL.GetAll().ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<EventDetails> Get(string id)
        {
            var item = _eventBL.GetById(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] EventDetails eventDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _eventBL.InsertEvent(eventDetails);

            return CreatedAtAction("Get", new { id = eventDetails.Id }, eventDetails);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] EventDetails eventDetails)
        {
            _eventBL.UpdateEvent(id, eventDetails);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {

            var existingItem = _eventBL.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            _eventBL.DeleteEvent(id);
            return Ok();
        }
    }
}
