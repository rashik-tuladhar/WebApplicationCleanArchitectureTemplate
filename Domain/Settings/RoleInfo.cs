using System.Collections.Generic;

namespace Domain.Settings
{
    public class RoleInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class RoleCommonDetails
    {
        public string Group { get; set; }
        public string SubGroupName { get; set; }
        public string MenuName { get; set; }
        public string Slug { get; set; }
        public string DisplayOrder { get; set; }
        public string IsActive { get; set; }
        public bool Selected { get; set; }
    }

    public class RoleDetailsLists
    {
        public string RoleName { get; set; }
        public string GroupName { get; set; }
        public string SubGroupName { get; set; }
        public List<RoleCommonDetails> RoleDetails { get; set; }
    }

    public class ViewModelRoleDetailsLists
    {
        public string RoleName { get; set; }
        public string SubGroupName { get; set; }
        public List<RoleDetailsLists> RoleLists { get; set; }
    }

    public class RoleUpdateViewDetails
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleDetailsLists> RoleLists { get; set; }
        public List<string> SelectedRoles { get; set; }
    }

    public class RoleUserInfo
    {
        public string CurrentRole { get; set; }
        public string Username { get; set; }
    }
}
