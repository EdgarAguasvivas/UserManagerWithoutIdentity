using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagerWithoutIdentityLogic.Models;
using UserManagerWithoutIdentityLogic.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace UserManagerWithoutIdentityLogic.Helpers
{
    public class UserHelper : IUserHelper
    {       
        private DataContext _Context;
        private IHttpContextAccessor _httpContext;

        public UserHelper(DataContext context,IHttpContextAccessor httpContextAccessor)
        {           
            _Context = context;
            _httpContext = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var userdetails = await _Context.User.Where(u => u.Email == model.Username && u.Password == model.Password).FirstOrDefaultAsync();

            if (userdetails == null)
            {
                return false;
            }
            return true;            
        }

        public async void UserAuthenticate(LoginViewModel model)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username)
                    };
            var identity = new ClaimsIdentity(
                claims, "Login");
             await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
