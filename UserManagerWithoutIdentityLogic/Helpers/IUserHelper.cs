using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagerWithoutIdentityLogic.Models;

namespace UserManagerWithoutIdentityLogic.Helpers
{
    public interface IUserHelper
    {
        Task<bool> LoginAsync(LoginViewModel model);
        void UserAuthenticate(LoginViewModel model);
    }
}
