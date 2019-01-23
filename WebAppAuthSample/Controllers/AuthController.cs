using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppAuthSample.Models;
using WebAppAuthSample.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAuthSample.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("signin")]
        public IActionResult SignIn()
        {

            return View(new UserModel());
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            UserModel user;
            if (await _userService.AuthenticateUser(model.UserName, model.Password, out user))
            {
                // ClaimPrinciple
                var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, model.UserName) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(claimPrincipal);
                return RedirectToAction("Index", "Home");
            }
            return View(new UserModel());
        }

        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            _userService.IsUserLogin = false;
            _userService.UserName = string.Empty;
            return RedirectToAction("SignIn","Auth");
        }
    }
}
