using System.Collections.Generic;
using Application.DTOs.Authentication.UserDTOs;
using Application.DTOs.Common;
using Domain.Settings;

namespace Application.Interfaces.CoreSetup.UserManagement
{
    public interface IUserManagementRepository
    {
        List<UserListDetails> GetUserLists(string storedProcedureName, GridParam gridParam);
        UserViewModelDetails GetRequiredDetails(string storedProcedureName, CommonDetails commonDetails);
    }
}
