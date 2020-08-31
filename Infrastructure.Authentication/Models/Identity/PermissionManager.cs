using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Authentication.Models.Identity
{
    /// <summary>
    /// Holds permission values for claims
    /// </summary>
    public static class PermissionManager
    {

        /**********************Need to add value in Utility Helper-> RoleExtension -> Permission Value Lists Compulsorily******************************/


        /*
         * Note: The string value must be specific
                setup.userlist|Setup|User.Management|mdi.mdi-account|Y|2|genericIcon|Link
                setup.userlist => Slug Name
                Setup => Menu Group Name
                || => Sub Group
                User.Management => Menu Name To Be Shown On Application
                mdi.mdi-account => Material Menu Icon
                Y => Is the menu to be shown on the application
                1 => Order of Menu
                mdi mdi-view-dashboard => Main Group Icon For Initial Menu
                CoreSetup/User/Index => Link to the Menu
                User Permission => Group Menu Name Acts As Header For Sub Menu
         */

        public const string ViewDashboard = "dashboard.view|Dashboard||Dashboard|mdi mdi-account|Y|1|mdi mdi-view-dashboard||"; //Do not delete this role


        /****** System Setup ******/
        //User Setup
        public const string ViewUserList = "AA1|System Setup||User Management|mdi mdi-account-group|Y|2|mdi mdi-settings-outline|CoreSetup/User/Index|User Permission";
        public const string AddUser = "AA2|System Setup||Add User|mdi mdi-account|N|3|mdi mdi-settings-outline||User Permission";
        public const string EditUser = "AA3|System Setup||Edit User|mdi mdi-account|N|4|mdi mdi-settings-outline||User Permission";
        public const string StatusUser = "AA4|System Setup||User Status|mdi mdi-account|N|6|mdi mdi-settings-outline||User Permission";
        //public const string DeleteUser = "AA5|System Setup||Delete User|mdi mdi-account|N|5|mdi mdi-settings-outline||User Permission";
        //Role Setup
        public const string AssignRoleUser = "AB1|System Setup||Assign Role|mdi mdi-account|N|7|mdi mdi-settings-outline||Role Permission";
        public const string ViewRoleList = "AB2|System Setup||Role Management|mdi mdi-account-key|Y|8|mdi mdi-settings-outline|CoreSetup/Role/Index|Role Permission";
        public const string AddRole = "AB3|System Setup||Add Role|mdi mdi-account-key|N|9|mdi mdi-settings-outline|CoreSetup/Role/AddRole|Role Permission";
        public const string EditRole = "AB4|System Setup||Edit Role|mdi mdi-account-key|N|10|mdi mdi-settings-outline||Role Permission";
        //public const string DeleteRole = "AB5|System Setup||Delete Role|mdi mdi-account-key|N|11|mdi mdi-settings-outline||Role Permission";

    }
    /// <summary>
    /// Creates and return list of string associated properties with permission manager class
    /// </summary>
    public static class PermissionsLists
    {
        public static List<string> GetLists()
        {
            Type type = typeof(PermissionManager);
            List<string> permissionLists = type.GetFields().Select(x => x.GetValue(null).ToString()).ToList();
            return permissionLists;
        }
    }
}
