using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Authentication.UserDTOs;
using Application.DTOs.Common;
using Application.Interfaces.CoreSetup.UserManagement;
using Application.Interfaces.Shared;
using AutoMapper;
using Domain.Settings;
using Infrastructure.Authentication.Contexts;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using WebApplication.Extensions;

namespace WebApplication.Areas.CoreSetup.Controllers
{
    [Area("CoreSetup")]
    [Route("CoreSetup/[controller]/[action]/{id?}")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger _log = Log.ForContext<UserController>();
        private readonly IUserManagementBusiness _userManagementBusiness;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(IUserManagementBusiness userManagementBusiness, IConfiguration configuration, IEncryptionService encryptionService, UserManager<ApplicationUser> userManager, AuthenticationDbContext context, IMapper mapper)
        {
            _userManagementBusiness = userManagementBusiness;
            _configuration = configuration;
            _encryptionService = encryptionService;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// View user list that are currently available
        /// </summary>
        /// <returns></returns>
        [Permission(PermissionManager.ViewUserList)]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// User List Grid Managed
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetGridDetails(GridDetails param)
        {
            _log.Information("Get User Lists For Grid");
            var gridParam = _mapper.Map<GridParam>(param);
            gridParam.Flag = "UserLists";
            gridParam.UserName = User.Identity.Name;

            _log.Information("User Lists With Parameters {0}", JsonConvert.SerializeObject(gridParam));
            var userLists = await _userManagementBusiness.GetUserLists(gridParam);
            _log.Information("User Lists Response with details {0}", JsonConvert.SerializeObject(userLists));
            var result = JsonConvert.SerializeObject(userLists);
            return result;
        }

        /// <summary>
        /// Page For Edit User
        /// </summary>
        /// <returns></returns>
        public IActionResult ManageUserView()
        {
            CommonDetails commonDetails = new CommonDetails();
            commonDetails.Flag = "GetRequiredDetails";
            var userViewModel = _userManagementBusiness.GetRequiredDetails(commonDetails);
            return View(userViewModel);
        }

        /// <summary>
        /// Check Username For Duplication
        /// </summary>
        /// <param name="commonDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CheckUserName(CommonDetails commonDetails)
        {
            commonDetails.Flag = "CheckUserName";
            _log.Information("Check username for duplications with parameters {0}", JsonConvert.SerializeObject(commonDetails));
            var response = await _userManagementBusiness.CheckUserName(commonDetails);
            return Json(response);
        }

        /// <summary>
        /// Add Identity User
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Permission(PermissionManager.AddUser)]
        public async Task<IActionResult> AddUser(UserViewModel userViewModel)
        {
            _log.Information("Create user with the parameter from the form as :", userViewModel);
            var user = new ApplicationUser
            {
                UserName = userViewModel.UserName.Replace(" ", "").Trim(),
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                FullName = string.Concat(userViewModel.FirstName, string.IsNullOrEmpty(userViewModel.MiddleName) ? "" : " " + userViewModel.MiddleName, " " + userViewModel.LastName),
                CreatedBy = User.Identity.Name,
                PhoneNumber = userViewModel.PhoneNumber,
                Gender = userViewModel.Gender,
                CreatedDate = DateTime.Now
            };
            _log.Information("Create user with the parameter as :", user);
            var password = string.IsNullOrEmpty(userViewModel.Password) ? _configuration["ApplicationData:DefaultPassword"] : userViewModel.Password;
            var result = await _userManager.CreateAsync(user, password);
            _log.Information("Result obtained for user creation as :", result);
            if (result.Succeeded)
            {
                _log.Information("Assign user role once user creation success, role to be assigned is : ", userViewModel.Role);
                ApplicationUser userDetails = await _userManager.FindByNameAsync(userViewModel.UserName);
                await _userManager.AddToRoleAsync(userDetails, userViewModel.Role);
                return RedirectToAction("Index").WithAlertMessage("000", "User added successfully with the role " + userViewModel.Role);
            }

            CommonDetails commonDetails = new CommonDetails();
            commonDetails.Flag = "GetRequiredDetails";
            var userViewModelDetails = _userManagementBusiness.GetRequiredDetails(commonDetails);
            return View("ManageUserView", userViewModelDetails).WithAlertMessage("111", result.Errors.ToString());
        }

        /// <summary>
        /// Get update details via code and return with form view
        /// </summary>
        /// <param name="id">identification term for the particular row</param>
        /// <returns></returns>
        [Permission(PermissionManager.EditUser)]

        public IActionResult UpdateUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Index").WithAlertMessage("111", "Update request could not be processed.");
            }
            CommonDetails commonDetails = new CommonDetails
            {
                Id = _encryptionService.DecryptString(id),
                Flag = "GetUserDetails"
            };
            _log.Information("Get User details for update with the param :", commonDetails);
            var userDetails = _userManagementBusiness.GetUserUpdateDetails(commonDetails);
            _log.Information("User detail get as :", userDetails);
            if (userDetails != null)
            {
                userDetails.Id = _encryptionService.EncryptString(userDetails.Id);
            }
            return View("ManageUserView", userDetails);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Permission(PermissionManager.EditUser)]
        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            var userParam = _mapper.Map<UserParams>(userViewModel);
            userParam.Flag = "EditUser";
            userParam.ModifiedBy = User.Identity.Name;
            userParam.Id = _encryptionService.DecryptString(userViewModel.Id);

            var user = new ApplicationUser { UserName = userViewModel.UserName };

            if (!string.IsNullOrEmpty(userViewModel.Password))
            {
                var password = _userManager.PasswordHasher.HashPassword(user, userViewModel.Password);
                userParam.PasswordHash = password;
            }

            userParam.FullName =
                string.Concat(userViewModel.FirstName, string.IsNullOrEmpty(userViewModel.MiddleName) ? "" : " " + userViewModel.MiddleName, " " + userViewModel.LastName);

            _log.Information("Manage User initiated with parameter {0}", JsonConvert.SerializeObject(userParam));
            var response = _userManagementBusiness.ManageUser(userParam);
            string currentRoleName = "";
            if (response.Code == ResponseConstant.SuccessCode)
            {
                _log.Information("Update user success now updating role");
                var userRoleLists = _context.UserRoles.Where(x => x.UserId == userParam.Id).ToList();
                if (userRoleLists.Count > 0)
                {
                    var currentRoleId = _context.UserRoles.Where(x => x.UserId == userParam.Id).ToList()[0].RoleId;
                    currentRoleName = _context.Roles.Where(x => x.Id == currentRoleId).ToList()[0].Name;
                    _log.Information("Got current role id as :", currentRoleId);
                }

                var appUser = await _userManager.FindByIdAsync(userParam.Id);
                try
                {
                    _log.Information("Removing current role and adding new role selected by user");
                    if (!string.IsNullOrEmpty(currentRoleName))
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, currentRoleName);
                    }
                    await _userManager.AddToRoleAsync(appUser, userParam.Role);
                }
                catch (Exception e)
                {
                    _log.Error("Role update error, role could not be updated ", e.Message);
                }

            }
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }

        /// <summary>
        /// Update User Status On Button Click
        /// </summary>
        /// <returns></returns>
        [Permission(PermissionManager.StatusUser)]
        public IActionResult UpdateUserStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Index").WithAlertMessage("111", "Status update request could not be processed.");
            }
            UserParams userParams = new UserParams
            {
                Id = _encryptionService.DecryptString(id),
                Flag = "UpdateUserStatus",
                ModifiedBy = User.Identity.Name
            };
            var response = _userManagementBusiness.UpdateUserStatus(userParams);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <returns></returns>
        [Permission(PermissionManager.EditUser)]
        public async Task<IActionResult> ResetUserPassword(string id)
        {
            var randomNewPassword = "";
            if (string.IsNullOrEmpty(id))
            {
                return View("Index").WithAlertMessage("111", "Reset password request could not be processed.");
            }
            var rowId = _encryptionService.DecryptString(id);
            var user = await _userManager.FindByIdAsync(rowId);
            if (user != null)
            {
                var resetResult = await _userManager.GeneratePasswordResetTokenAsync(user);
                randomNewPassword = GeneralUtility.RandomPassword();
                var result = await _userManager.ResetPasswordAsync(user, resetResult, randomNewPassword);
            }
            else
            {
                return View("Index").WithAlertMessage(ResponseConstant.ErrorCode, "Password reset failed. User didn't exist.");
            }
            return View("Index").WithAlertMessage(ResponseConstant.SuccessCode, "User password has been reset successfully. The new password is "+ randomNewPassword);
        }
    }
}
