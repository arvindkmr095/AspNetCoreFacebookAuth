using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppAuthSample.Models;

namespace WebAppAuthSample.Services
{
    public class UserService : IUserService
    {
        private IDictionary<string, (string PasswordHash, UserModel User)> _users =
            new Dictionary<string, (string PasswordHash, UserModel User)>();

        private bool _isUserLogin;
        private string _userName;

        public UserService(IDictionary<string, string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new UserModel() { UserName = user.Key }));
            }


        }

        public bool IsUserLogin { get => _isUserLogin; set => _isUserLogin = value; }
        public string UserName { get => _userName; set => _userName = value; }

        public Task<bool> AuthenticateUser(string userName, string password, out UserModel user)
        {
            user = null;
            var key = userName.ToLower();
            if (_users.ContainsKey(userName.ToLower()))
            {
                var hash = _users[key].PasswordHash;
                if (BCrypt.Net.BCrypt.Verify(password, hash))
                {
                    user = new UserModel() { UserName = userName };
                    _userName = userName;
                    _isUserLogin = true;
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);

        }
    }
}
