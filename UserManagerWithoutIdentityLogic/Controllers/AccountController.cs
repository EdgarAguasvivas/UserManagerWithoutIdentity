using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagerWithoutIdentityLogic.Helpers;
using UserManagerWithoutIdentityLogic.Models;

namespace UserManagerWithoutIdentityLogic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;               
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            ILogger<AccountController> logger,
            IUserHelper userHelper
            )
        {
            _userHelper = userHelper;           
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result == true)
                {
                    _userHelper.UserAuthenticate(model);
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.error = "Usuario o Contraseña invalida";
                model.Password = string.Empty;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }      
    }
}
