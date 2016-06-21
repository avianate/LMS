using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}