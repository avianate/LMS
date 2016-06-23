using LMS.Entities;
using LMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }


        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        return Redirect(returnUrl);
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Incorrect UserName or Password");
                }
            }

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
