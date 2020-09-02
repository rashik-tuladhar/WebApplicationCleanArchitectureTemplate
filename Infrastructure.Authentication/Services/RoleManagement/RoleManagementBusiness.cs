using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Interfaces.CoreSetup.RoleManagement;
using Application.Interfaces.Shared;
using Domain.Settings;
using Infrastructure.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Authentication.Services.RoleManagement
{
    public class RoleManagementBusiness : IRoleManagementBusiness
    {
        private readonly ILogger _log = Log.ForContext<RoleManagementBusiness>();
        private readonly IRoleManagementRepository _roleManagementRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _configuration;
        private readonly IRoleExtension _roleExtension;
        //Stored Procedure For Role Management Controller
        const string StoredProcedureName = "PROC_ROLEMANAGEMENT";

        public RoleManagementBusiness(IRoleManagementRepository roleManagementRepository, IConfiguration configuration, IRoleExtension roleExtension, IEncryptionService encryptionService)
        {
            _roleManagementRepository = roleManagementRepository;
            _configuration = configuration;
            _roleExtension = roleExtension;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Get All The Available Role Lists
        /// </summary>
        /// <returns></returns>
        public List<RoleDetailsLists> GetAvailableRoleLists()
        {
            var roleLists = _roleManagementRepository.GetAvailableRoleLists(StoredProcedureName);
            var roleGroup = roleLists.GroupBy(x => x.SubGroupName).Select(g => g.First()).ToList();
            List<RoleDetailsLists> roleDetailsLists = new List<RoleDetailsLists>();
            foreach (var group in roleGroup)
            {
                RoleDetailsLists roleDetailsList = new RoleDetailsLists();
                roleDetailsList.GroupName = group.Group;
                roleDetailsList.SubGroupName = group.SubGroupName;
                roleDetailsList.RoleDetails = roleLists.Where(x => x.SubGroupName == group.SubGroupName).ToList();
                roleDetailsLists.Add(roleDetailsList);
            }
            return roleDetailsLists;
        }

        public HtmlGrid<RoleLists> GetRoleLists(GridParam roleDetails)
        {
            List<RoleLists> roleLists = new List<RoleLists>();
            var details = _roleManagementRepository.GetRoleLists(roleDetails, StoredProcedureName);
            foreach (RoleLists role in details)
            {
                var detail = new RoleLists()
                {
                    Name = role.Name,
                    FilterCount = role.FilterCount
                };
                var roleId = _encryptionService.EncryptString(role.Id);
                StringBuilder actionDetails = new StringBuilder();
                if (_roleExtension.HasPermission(PermissionValueLists.EditRole))
                {
                    actionDetails.Append("<a href='" + _configuration["ApplicationData:RootUrl"] + "/CoreSetup/Role/EditRole/" + roleId + "' class='btn btn-sm btn-link btn-round' title='Edit Role'><i class=\"fas fa-user-edit\"></i></a>");
                }
                detail.Action = actionDetails.ToString();
                roleLists.Add(detail);
            }
            _log.Information("role list get list response as {0}", JsonConvert.SerializeObject(roleLists));
            var roleList = new HtmlGrid<RoleLists>();
            roleList.aaData = roleLists;
            var firstDefault = roleLists.FirstOrDefault();
            if (firstDefault != null)
            {
                roleList.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                roleList.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }
            return roleList;
        }

        public RoleUpdateViewDetails GetRoleDetailsUpdate(string roleId)
        {
            roleId = _encryptionService.DecryptString(roleId);
            var roleDetails = _roleManagementRepository.GetRoleDetailsUpdate(roleId, StoredProcedureName);
            var roleLists = roleDetails.Item1;
            var roleGroup = roleLists.GroupBy(x => x.SubGroupName).Select(g => g.First()).ToList();
            List<RoleDetailsLists> roleDetailsLists = new List<RoleDetailsLists>();
            foreach (var group in roleGroup)
            {
                RoleDetailsLists roleDetailsList = new RoleDetailsLists();
                roleDetailsList.GroupName = group.Group;
                roleDetailsList.SubGroupName = group.SubGroupName;
                roleDetailsList.RoleDetails = roleLists.Where(x => x.SubGroupName == group.SubGroupName).ToList();
                roleDetailsLists.Add(roleDetailsList);
            }
            RoleUpdateViewDetails roleUpdateViewDetails = new RoleUpdateViewDetails();
            roleUpdateViewDetails.RoleLists = roleDetailsLists;
            roleUpdateViewDetails.SelectedRoles = roleDetails.Item2;
            roleUpdateViewDetails.RoleId = roleDetails.Item3.Id;
            roleUpdateViewDetails.RoleName = roleDetails.Item3.Name;
            return roleUpdateViewDetails;
        }
    }
}
