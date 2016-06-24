using LMS.Entities;
using LMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LMS.Controllers
{
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


        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("~/Views/Home/Index.cshtml");
            }

            return PartialView("SignIn");
        }

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
                        return Json(vm.UserName);
                    }

                    else
                    {
                        return Json(false);
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Incorrect UserName or Password");
                }
            }

            return Json(false);
        }

        public async Task<IActionResult> SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return PartialView("~/Views/Home/Index.cshtml");
        }
    }
}
