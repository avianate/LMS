using System.Collections.Generic;
using LMS.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        private readonly ILMSRepository _repo;

        public DataController(ILMSRepository repo)
        {
            _repo = repo;
        }

        // GET: api/data
        [HttpGet]
        public JsonResult Get()
        {
            var posts = _repo.GetAllPublishedPosts();
            var json = Json(posts);

            return json;
        }

        [HttpGet]
        public JsonResult GetAllPosts()
        {
            var posts = _repo.GetAllPosts();

            return Json(posts);
        }

        // GET api/data/5
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            var theId = id.ToString();
            return new string[] { $"{theId}" };
        }

        // POST api/data
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/data/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
