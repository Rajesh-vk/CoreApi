using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API_MODEL;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FSE_DataAccess.Implementation;
using FSE_DataAccess.Interfaces;

namespace FSE_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize()]
    public class API1Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                //IEventRepo sampleRepo = new EventRepo();
                //return sampleRepo.GetById("1");
                return "value";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            //Family andersenFamily = new Family
           // {
           //     Id = "Andersen.1",
           //     LastName = "Andersen",
           //     Parents = new Parent[]
           //{
           //     new Parent { FirstName = "Thomas" },
           //     new Parent { FirstName = "Mary Kay" }
           //},
           //     Children = new Child[]
           //{
           // new Child
           // {
           //     FirstName = "Henriette Thaulow",
           //     Gender = "female",
           //     Grade = 5,
           //     Pets = new Pet[]
           //     {
           //         new Pet { GivenName = "Fluffy" }
           //     }
           // }
           //},
           //     Address = new Address { State = "WA", County = "King", City = "Seattle" },
           //     IsRegistered = false
           // };

           // try
           // {
           //     IEventRepo sampleRepo = new EventRepo();
           //     var andersenFamilyResponse = sampleRepo.GetById(andersenFamily.Id);
           //     if (andersenFamilyResponse != null)
           //         sampleRepo.Insert(andersenFamily);
           // }
           // catch (Exception ex)
           // {

           // }
        }

        // PUT api/values/5
        [HttpPost("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpPost("{id}")]
        public void Delete(int id)
        {
        }
    }
}
