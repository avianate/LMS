using LMS.Entities;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("~/Views/Home/Index.cshtml");
            }

            return PartialView("SignIn");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> SignIn([FromBody]LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Json(new { userName = vm.UserName });
                    }

                    else
                    {
                        return Json(new { url = returnUrl, userName = vm.UserName });
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Incorrect UserName or Password");
                }
            }

            var result = (int)HttpStatusCode.Unauthorized;
            Response.StatusCode = result;

            return Json(result);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return PartialView("~/Views/Home/Index.cshtml");
        }


        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = GetCurrentUserASync().Result;


                var profile = new UserProfileViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                return Json(profile);
            }

            var result = (int)HttpStatusCode.Unauthorized;
            Response.StatusCode = result;

            return Json(result);
        }

        private Task<User> GetCurrentUserASync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
