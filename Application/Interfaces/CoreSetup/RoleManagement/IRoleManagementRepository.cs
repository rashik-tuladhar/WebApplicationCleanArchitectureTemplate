using System;
using System.Collections.Generic;
using Domain.Settings;

namespace Application.Interfaces.CoreSetup.RoleManagement
{
    public interface IRoleManagementRepository
    {
        List<RoleCommonDetails> GetAvailableRoleLists(string storedProcedureName);
        List<RoleLists> GetRoleLists(GridParam roleDetails, string storedProcedureName);
        Tuple<List<RoleCommonDetails>, List<string>, RoleInfo> GetRoleDetailsUpdate(string roleId, string storedProcedureName);
    }
}
