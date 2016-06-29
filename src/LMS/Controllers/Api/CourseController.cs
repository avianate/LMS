using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Data;

namespace LMS.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly ILMSRepository _repo;

        public CourseController(ILMSRepository repo)
        {
            _repo = repo;
        }

        // GET: api/data
        [HttpGet]
        public JsonResult Get()
        {
            var posts = _repo.GetAllPublishedCourses();
            var json = Json(posts);

            return json;
        }

        [HttpGet]
        public JsonResult GetAllCourses()
        {
            var posts = _repo.GetAllCourses();

            return Json(posts);
        }

        [HttpGet]
        public JsonResult Latest()
        {
            var posts = _repo.GetAllPublishedCourses().Take(20);

            return Json(posts);
        }
    }
}