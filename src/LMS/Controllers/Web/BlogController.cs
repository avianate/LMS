using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers.Web
{
    public class BlogController : Controller
    {
        public IActionResult Index () 
        {
            return View();
        }
    }
}