using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers.Api
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "firstValue", "secondValue" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            var theId = id.ToString();
            return new string[] { $"{theId}" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
