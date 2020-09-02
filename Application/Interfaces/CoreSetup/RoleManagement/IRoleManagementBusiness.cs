using System.Collections.Generic;
using Domain.Settings;

namespace Application.Interfaces.CoreSetup.RoleManagement
{
    public interface IRoleManagementBusiness
    {
        List<RoleDetailsLists> GetAvailableRoleLists();
        HtmlGrid<RoleLists> GetRoleLists(GridParam roleDetails);
        RoleUpdateViewDetails GetRoleDetailsUpdate(string roleId);
    }
}
