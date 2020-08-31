using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.Interfaces.Repositories;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.Authentication.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApplication.Extensions;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGenericRepositoryDapper _genericRepository;

        public LoginController(SignInManager<ApplicationUser> signInManager, IGenericRepositoryDapper genericRepository)
        {
            _signInManager = signInManager;
            _genericRepository = genericRepository;
        }

        public IActionResult Index(string returnUrl = null)
        {
            return View();
        }

        /// <summary>
        /// Verify User and provides access to dashboard page
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View("Index").WithAlertMessage(ResponseConstant.ErrorCode, "Invalid Credentials! Either your username or password is incorrect. Please try again.");
                }
            }

            return View("Index").WithAlertMessage(ResponseConstant.ErrorCode, "Please fill both the required fields.");
        }


        /// <summary>
        /// Logout of the system
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index").WithAlertMessage(ResponseConstant.SuccessCode, "Logged out successfully");
        }
    }
}
