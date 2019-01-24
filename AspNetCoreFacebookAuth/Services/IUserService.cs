using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFacebookAuth.Models;

namespace AspNetCoreFacebookAuth.Services
{
    public interface IUserService
    {
        bool IsUserLogin { get; set; }
        string UserName { get; set; }
        Task<bool> AuthenticateUser(string userName, string password, out UserModel user);
    }
}
