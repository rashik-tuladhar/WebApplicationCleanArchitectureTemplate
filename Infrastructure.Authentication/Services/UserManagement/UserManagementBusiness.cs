using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Authentication.UserDTOs;
using Application.DTOs.Common;
using Application.Interfaces.CoreSetup.RoleManagement;
using Application.Interfaces.CoreSetup.UserManagement;
using Application.Interfaces.Repositories;
using Application.Interfaces.Shared;
using Domain.Settings;
using Infrastructure.Shared.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Authentication.Services.UserManagement
{
    public class UserManagementBusiness : IUserManagementBusiness
    {
        private readonly ILogger _log = Log.ForContext<UserManagementBusiness>();
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IGenericRepositoryDapper _genericRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _configuration;
        private readonly IRoleExtension _roleExtension;
        const string StoredProcedureName = "PROC_USERMANAGEMENT";

        public UserManagementBusiness(IUserManagementRepository userManagementRepository, IGenericRepositoryDapper genericRepository, IEncryptionService encryptionService, IConfiguration configuration, IRoleExtension roleExtension)
        {
            _userManagementRepository = userManagementRepository;
            _genericRepository = genericRepository;
            _encryptionService = encryptionService;
            _configuration = configuration;
            _roleExtension = roleExtension;
        }

        public async Task<HtmlGrid<UserListDetails>> GetUserLists(GridParam gridParam)
        {
            List<Task<UserListDetails>> tasksUserlists = new List<Task<UserListDetails>>();
            var details = _userManagementRepository.GetUserLists(StoredProcedureName, gridParam);
            foreach (UserListDetails user in details)
            {
                tasksUserlists.Add(Task.Run(() => UserGridManagement(user)));
            }

            var results = await Task.WhenAll(tasksUserlists);

            var userList = results.ToList();
            _log.Information("User details got as {0}", JsonConvert.SerializeObject(userList));
            var userLists = new HtmlGrid<UserListDetails> {aaData = userList};
            var firstDefault = userList.FirstOrDefault();
            if (firstDefault != null)
            {
                userLists.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                userLists.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }

            return userLists;
        }

        public async Task<SystemResponse> CheckUserName(CommonDetails commonDetails)
        {
            var systemResponse = await _genericRepository.ManageDataWithSingleObjectAsync<SystemResponse>(StoredProcedureName,
                commonDetails);
            return systemResponse;
        }

        public UserViewModel GetRequiredDetails(CommonDetails commonDetails)
        {
            UserViewModel userViewModelReturn = new UserViewModel();
            var results = _userManagementRepository.GetRequiredDetails(StoredProcedureName, commonDetails);
            userViewModelReturn.GenderList = results.ListGender.Select(x => new SelectListItem { Value = x.Value, Text = x.Description }).ToList();
            userViewModelReturn.RoleList = results.ListRoles.Select(x => new SelectListItem { Value = x.Value, Text = x.Description }).ToList();
            return userViewModelReturn;
        }

        public UserViewModel GetUserUpdateDetails(CommonDetails commonDetails)
        {
            var resultUpdateDetails = _genericRepository.ManageDataWithSingleObject<UserViewModel>(StoredProcedureName, commonDetails);
            var results = _userManagementRepository.GetRequiredDetails(StoredProcedureName, new CommonDetails{ Flag = "GetRequiredDetails" });
            resultUpdateDetails.GenderList = results.ListGender.Select(x => new SelectListItem { Value = x.Value, Text = x.Description }).ToList();
            resultUpdateDetails.RoleList = results.ListRoles.Select(x => new SelectListItem { Value = x.Value, Text = x.Value }).ToList();
            try
            {
                if (!string.IsNullOrEmpty(resultUpdateDetails.Gender))
                    resultUpdateDetails.GenderList.First(x => x.Value == resultUpdateDetails.Gender).Selected = true;
                if (!string.IsNullOrEmpty(resultUpdateDetails.Role))
                    resultUpdateDetails.RoleList.First(x => x.Value == resultUpdateDetails.Role).Selected = true;
            }
            catch (Exception e)
            {
                _log.Error("Some ddl value missing ", e.StackTrace);
            }
            if (string.IsNullOrEmpty(resultUpdateDetails.Status))
            {
                resultUpdateDetails.Status = "A";
            }
            return resultUpdateDetails;
        }

        public SystemResponse ManageUser(UserParams userParam)
        {
            return _genericRepository.ManageDataWithSingleObject<SystemResponse>(StoredProcedureName, userParam);
        }

        public SystemResponse UpdateUserStatus(UserParams userParam)
        {
            return _genericRepository.ManageDataWithSingleObject<SystemResponse>(StoredProcedureName, userParam);
        }

        /// <summary>
        /// Operation Related To Grid Manipulation (For Buttons And Other Functions)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserListDetails UserGridManagement(UserListDetails user)
        {
            var rowId = _encryptionService.EncryptString(Convert.ToString(user.Id));
            if (string.IsNullOrEmpty(user.Status))
            {
                user.Status = "A";
            }
            user.Status = user.Status.Trim() == "A" ? "<i class=\"mdi mdi-account-check mdi-18px text-success\" title='Unlocked'></i>" : "<i class=\"mdi mdi-account-off mdi-18px text-danger\" title='Locked'></i>";
            StringBuilder actionDetails = new StringBuilder();
            if (_roleExtension.HasPermission(PermissionValueLists.EditUser))
            {
                actionDetails.Append("<a href='" + _configuration["ApplicationData:RootUrl"] + "/CoreSetup/User/UpdateUser/" + rowId + "' class='btn btn-sm btn-link btn-round' title='Edit User'><i class='mdi mdi-pencil'></i></a>");
                actionDetails.Append(" <a href='" + _configuration["ApplicationData:RootUrl"] + "/CoreSetup/User/ResetUserPassword/" + rowId + "' class='btn btn-sm btn-warning btn-round confirmation' title='Reset User Password'><i class='mdi mdi-account-key'></i></a>");
            }
            if (_roleExtension.HasPermission(PermissionValueLists.StatusUser))
            {
                actionDetails.Append(" <a href='" + _configuration["ApplicationData:RootUrl"] + "/CoreSetup/User/UpdateUserStatus/" + rowId + "' class='btn btn-sm btn-success btn-round confirmation' title='Change Status'><i class='mdi mdi-lock-reset'></i></a>");
            }
            user.Action = actionDetails.ToString();
            return user;
        }
    }
}
