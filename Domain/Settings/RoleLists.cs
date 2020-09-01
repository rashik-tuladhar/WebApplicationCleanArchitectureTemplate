using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Settings
{
    public class RoleLists
    {
        public string FilterCount { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
    }

    public class RoleUpdateInfo
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string CurrentRole { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public string CreatedBy { get; set; }
    }
}
