using UserManagerWithoutIdentityLogic.Controllers;
using UserManagerWithoutIdentityLogic.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace UserManagerWithoutIdentityLogic.Helpers
{
    public class FilterAuthenticate : IActionFilter
    {     
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {                
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!(context.Controller is AccountController))
                    {
                        context.HttpContext.Response.Redirect("/Account/Login");
                    }
                }
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
