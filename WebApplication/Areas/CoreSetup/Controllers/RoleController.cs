using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces.CoreSetup.RoleManagement;
using Domain.Settings;
using Infrastructure.Authentication.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using WebApplication.Extensions;

namespace WebApplication.Areas.CoreSetup.Controllers
{
    [Area("CoreSetup")]
    [Route("CoreSetup/[controller]/[action]/{id?}")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly ILogger _log = Log.ForContext<RoleController>();
        private readonly IRoleManagementBusiness _roleManagementBusiness;
        private readonly IServiceProvider _serviceProvider;

        public RoleController(IRoleManagementBusiness roleManagementBusiness, IServiceProvider serviceProvider)
        {
            _roleManagementBusiness = roleManagementBusiness;
            _serviceProvider = serviceProvider;
        }

        [Permission(PermissionManager.ViewRoleList)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Role List Api For Getting Grid Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetGridDetails(GridDetails param)
        {
            _log.Information("Get Role Lists For Grid");
            var roleDetails = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetRoleLists",
                Search = param.search.value,
                UserName = User.Identity.Name
            };
            _log.Information("Role Lists With Parameters {0}", JsonConvert.SerializeObject(roleDetails));
            var roleList = _roleManagementBusiness.GetRoleLists(roleDetails);
            _log.Information("Role Lists Response with details {0}", JsonConvert.SerializeObject(roleList));
            var roleLists = new HtmlGrid<RoleLists>();
            roleLists.aaData = roleList;
            var firstDefault = roleList.FirstOrDefault();
            if (firstDefault != null)
            {
                roleLists.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                roleLists.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }

            var result = JsonConvert.SerializeObject(roleLists);
            return result;
        }

        /// <summary>
        ///     Display Add New Role Page
        /// </summary>
        /// <returns></returns>
        [Permission(PermissionManager.AddRole)]
        public IActionResult AddRole()
        {
            _log.Information("Add Role Page Initiated And Get Role Lists");
            var roleDetailLists = _roleManagementBusiness.GetAvailableRoleLists();
            _log.Information("Role Details Get As {0}", JsonConvert.SerializeObject(roleDetailLists));
            var viewModelRoleDetailsLists = new ViewModelRoleDetailsLists();
            viewModelRoleDetailsLists.RoleLists = roleDetailLists;
            return View(viewModelRoleDetailsLists);
        }

        /// <summary>
        ///     Add New Role Group
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission(PermissionManager.AddRole)]
        public async Task<IActionResult> AddRole(ViewModelRoleDetailsLists viewModelRoleDetailsLists)
        {
            _log.Information("Add Role Group With Parameters {0}",
                JsonConvert.SerializeObject(viewModelRoleDetailsLists));
            if (ModelState.IsValid)
            {
                viewModelRoleDetailsLists.RoleName = viewModelRoleDetailsLists.RoleName.ToUpper();
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                _log.Information("Get All Roles To Check If Role Group Present {0}",
                    JsonConvert.SerializeObject(roleManager));
                var roleLists = Request.Form["eachRoleValue"].ToList();
                if (!roleLists.Contains("dashboard.view"))
                {
                    roleLists.Add("dashboard.view");
                }
                var roleExist = await roleManager.RoleExistsAsync(viewModelRoleDetailsLists.RoleName);
                if (!roleExist)
                {
                    try
                    {
                        await roleManager.CreateAsync(new IdentityRole(viewModelRoleDetailsLists.RoleName));
                        _log.Information("Role Add Success Now Inserting Claims");
                        foreach (var roleClaim in roleLists)
                        {
                            //get claims assoc with role
                            var roleDetails = await roleManager.FindByNameAsync(viewModelRoleDetailsLists.RoleName);
                            //claims list assoc with roles above mentioned
                            var roles = await roleManager.GetClaimsAsync(roleDetails);
                            if (roleDetails != null)
                            {
                                var role = Convert.ToString(roles.FirstOrDefault(x => x.Value == roleClaim));
                                if (role == "")
                                    await roleManager.AddClaimAsync(roleDetails, new Claim("permission", roleClaim));
                            }
                        }

                        _log.Information("Role Add Success | Claim Add Success");
                        return RedirectToAction("Index").WithSuccess("Success", "Roles added successfully.");
                    }
                    catch (Exception e)
                    {
                        _log.Information("Exception Occured While Inserting Role With Message {0}", e.Message);
                        return RedirectToAction("AddRole").WithDanger("Failed", e.Message);
                    }
                }
                else
                {
                    _log.Information("Add Role Failed The role name already exists. Please use any other role name.");
                    return RedirectToAction("AddRole").WithAlertMessage("111",
                        "The role name already exists. Please use any other role name.");
                }
            }

            _log.Information("Add Role Failed Failed Validation.");
            return RedirectToAction("AddRole").WithDanger("Failed", "Failed Validation");
        }

        /// <summary>
        ///     Get Role Details For Update
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[Permission(PermissionManager.EditRole)]
        public IActionResult EditRole(string id)
        {
            var roleDetails = _roleManagementBusiness.GetRoleDetailsUpdate(id);
            return View(roleDetails);
        }

        /// <summary>
        ///     Update Role Add New And Delete if Unticked
        /// </summary>
        /// <param name="roleUpdateViewDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission(PermissionManager.EditRole)]
        public async Task<IActionResult> EditRoles(RoleUpdateViewDetails roleUpdateViewDetails)
        {
            if (ModelState.IsValid)
            {
                //get all role lists firs
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleLists = Request.Form["eachRoleValue"].ToList();

                var roleExist = await roleManager.RoleExistsAsync(roleUpdateViewDetails.RoleName);
                if (roleExist)
                    try
                    {
                        //get claims assoc with role
                        var roleDetails = await roleManager.FindByIdAsync(roleUpdateViewDetails.RoleId);
                        //claims list assoc with roles above mentioned
                        var roles = await roleManager.GetClaimsAsync(roleDetails);
                        var currentRoles = new List<string>();
                        foreach (var role in roles) currentRoles.Add(role.Value);

                        var deletedRoles = currentRoles.Except(roleLists).ToList();
                        foreach (var deleteRole in deletedRoles)
                            await roleManager.RemoveClaimAsync(roleDetails, new Claim("permission", deleteRole));

                        if (!roleLists.Contains("dashboard.view"))
                        {
                            roleLists.Add("dashboard.view");
                        }
                        foreach (var roleClaim in roleLists)
                            if (roleDetails != null)
                            {
                                var role = Convert.ToString(roles.FirstOrDefault(x => x.Value == roleClaim));
                                if (role == "" || roleClaim == "dashboard.view")
                                    await roleManager.AddClaimAsync(roleDetails, new Claim("permission", roleClaim));
                            }
                            else
                            {
                                return RedirectToAction("EditRole")
                                    .WithDanger("Failed", "No role exists with the role name.");
                            }

                        return RedirectToAction("Index").WithSuccess("Success", "Roles updated successfully.");
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("EditRole").WithDanger("Failed", e.Message);
                    }

                return RedirectToAction("EditRole").WithDanger("Failed", "No role exists with the role name.");
            }

            return RedirectToAction("EditRole").WithDanger("Failed", "Failed Validation");
        }
    }
}
