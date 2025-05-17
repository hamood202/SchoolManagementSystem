using BL.Dto;
using BL.Interfase;
using DataAccessLayer.UserModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Sevices;
using Ui.Models;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GenericApiClient _genericApiClient;
        public AccountController(IUserService userService, SignInManager<ApplicationUser> signInManager, GenericApiClient genericApiClient, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _genericApiClient = genericApiClient;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }
            var result = await _userService.LoginAsync(userDto);
            if (result.Success)
            {
                LoginApiModel apiResponse = await _genericApiClient.PostAsync<LoginApiModel>("api/auth/login", userDto);

                if (apiResponse == null)
                {

                    ModelState.AddModelError(string.Empty, "Api Error :  enable to process login.");
                    return View(userDto);
                }
                var accessToken = apiResponse.AccessToken?.ToString();
                if (string.IsNullOrEmpty(accessToken))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(userDto);
                }

                var dbUser = await _userService.GetUserByEmailAsync(userDto.Email);

                var user = await _userManager.GetUserAsync(User);
                var UserRoles = await _userManager.GetRolesAsync(user);
                dbUser.Role = UserRoles.FirstOrDefault();

                if (dbUser.Role.ToLower() == "admin")
                    return RedirectToRoute(new { area = "admin", controller = "Home", action = "Index" });

                else
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                return View(userDto);
            }
            var result = await _userService.RegisterUser(userDto);
            if (result.Success)
            {
                return RedirectToRoute(new { controller = "Account", action = "Login" });
            }
            else
                return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
