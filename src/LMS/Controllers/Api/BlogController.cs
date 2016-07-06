using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Data;

namespace LMS.Controllers
{
    [Route("api/[controller]/[action]/{slug?}")]
    public class BlogController : Controller
    {
        private readonly ILMSRepository _repo;

        public BlogController(ILMSRepository repo)
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

        [HttpGet]
        public JsonResult Latest()
        {
            var posts = _repo.GetAllPublishedPosts().Take(20);

            return Json(posts);
        }

        [HttpGet]
        public JsonResult Post()
        {
            var slug = RouteData.Values["slug"].ToString();
            var post = _repo.GetPublishedPost(slug);

            if (post == null)
            {
                return Json(false);
            }

            return Json(post);
        }
    }
}