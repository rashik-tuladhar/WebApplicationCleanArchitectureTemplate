using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.CoreSetup.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplication.Extensions;

namespace WebApplication.Areas.CoreSetup.Controllers
{
    [Area("CoreSetup")]
    [Route("CoreSetup/[controller]/[action]/{id?}")]
    [Authorize]
    public class CoreSetupUtilityController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _log = Log.ForContext<CoreSetupUtilityController>();

        public CoreSetupUtilityController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Opens Up View For Change Password
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Change Password After All The Criteria Meet
        /// </summary>
        /// <param name="passwordDetails">Old Password And New Password</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword passwordDetails)
        {
            var user = await _userManager.GetUserAsync(User);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, passwordDetails.OldPassword, passwordDetails.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    errorMessage.Append(error.Description);
                }
                _log.Information("Change password error.", errorMessage);
                return RedirectToAction("ChangePassword").WithAlertMessage(ResponseConstant.ErrorCode, errorMessage.ToString());
            }

            await _signInManager.SignOutAsync();
            _log.Information("User changed their password successfully.");
            return RedirectToAction("Index", "Login").WithAlertMessage(ResponseConstant.SuccessCode, "You have successfully changed your password. Please login with the new password to continue.");
        }
    }
}
