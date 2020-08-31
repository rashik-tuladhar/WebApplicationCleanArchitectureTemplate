using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Authentication.Contexts;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Extensions
{
    public static class PermissionInitializer
    {
        /// <summary>
        /// Initialize Permission , Add Permission Values To Claims Table And Assign Default Value To Super User
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task InitializePermission(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<AuthenticationDbContext>();
            string[] roleNames = { "ADMINISTRATOR", "DEFAULT" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            string userName = "SUPERUSER";
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            await userManager.AddToRoleAsync(user, "ADMINISTRATOR");
            var roleList = PermissionsLists.GetLists();

            var roleDetails = await roleManager.FindByNameAsync("ADMINISTRATOR");
            var roles = await roleManager.GetClaimsAsync(roleDetails);
            foreach (var roleClaim in roleList)
            {
                if (roleDetails != null)
                {
                    var claim = roleClaim.Split('|')[0];
                    var role = Convert.ToString(roles.FirstOrDefault(x => x.Value == claim));
                    if (role == "")
                    {
                        await roleManager.AddClaimAsync(roleDetails, new Claim("permission", claim));
                    }
                }
            }
            //add role permission to the table from permission class
            AddAuthRolePermissions(context);

        }


        /// <summary>
        /// Adds role permission to AuthRolePermission table which are not present
        /// </summary>
        /// <param name="context"></param>
        private static void AddAuthRolePermissions(AuthenticationDbContext context)
        {
            var existingRolePermission = context.RolePermission;
            var roleList = PermissionsLists.GetLists();
            foreach (var roleClaim in roleList)
            {
                string slug = roleClaim.Split('|')[0];
                string group = roleClaim.Split('|')[1];
                string subGroup = roleClaim.Split('|')[2];
                string menuName = roleClaim.Split('|')[3];
                string menuIcon = roleClaim.Split('|')[4];
                string menuIsActive = roleClaim.Split('|')[5];
                string order = roleClaim.Split('|')[6];
                string groupIcon = roleClaim.Split('|')[7];
                string link = roleClaim.Split('|')[8];
                string subGroupName = roleClaim.Split('|')[9];
                if (!existingRolePermission.Any(m => m.Slug == slug))
                {
                    var authRolePermission = new RolePermission
                    {
                        CreatedDate = DateTime.UtcNow,
                        Name = TextUtility.CamelCaseTranslation(menuName),
                        Slug = slug,
                        Group = TextUtility.CamelCaseTranslation(group),
                        SubGroup = string.IsNullOrEmpty(subGroup) ? "" : TextUtility.CamelCaseTranslation(subGroup),
                        MenuName = TextUtility.CamelCaseTranslation(menuName),
                        Icon = menuIcon,
                        IsActive = menuIsActive == "Y" ? true : false,
                        DisplayOrder = Convert.ToInt32(order),
                        GroupIcon = groupIcon,
                        Link = link,
                        SubGroupName = subGroupName
                    };
                    context.RolePermission.Add(authRolePermission);
                    context.SaveChanges();
                }
            }
        }

    }
}
