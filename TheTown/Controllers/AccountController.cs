using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTown.DAL;
using TheTown.Models;
using TheTown.ViewModels.AppUser;

namespace TheTown.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (!ModelState.IsValid) ;

            AppUser user = new AppUser
            {
                FirstName = userRegisterVM.FirstName,
                LastName = userRegisterVM.LastName,
                UserName = userRegisterVM.UserName,
                Email = userRegisterVM.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, userRegisterVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                };
                return View();
            }
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction(nameof(Login), "Account");
        }
        [HttpGet]
        public async Task<IActionResult> Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm userLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(userLoginVM.UserName);
            if (user == null) { ModelState.AddModelError("Username", "Yoxdu hewne"); return View(); }
            var result = await _signInManager.PasswordSignInAsync(user, userLoginVM.Password, userLoginVM.IsParsistance, true);
            return RedirectToAction(nameof(Index), "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
