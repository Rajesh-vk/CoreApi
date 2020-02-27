using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Mvc;

namespace EventServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return _eventBL.GetAll().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<EventDetails> Get(string id)
        {
            return _eventBL.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] EventDetails eventDetails)
        {
            _eventBL.InsertEvent(eventDetails);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] EventDetails eventDetails)
        {
            _eventBL.UpdateEvent(id, eventDetails);
        }

        // DELETE api/values/5
        [HttpPost("{id}")]
        public void Delete(string id)
        {
            _eventBL.DeleteEvent(id);
        }
    }
}
