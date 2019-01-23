using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAuthSample.Models;

namespace WebAppAuthSample.Services
{
    public interface IUserService
    {
        bool IsUserLogin { get; set; }
        string UserName { get; set; }
        Task<bool> AuthenticateUser(string userName, string password, out UserModel user);
    }
}
