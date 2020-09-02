using System.Threading.Tasks;
using Application.DTOs.Authentication.UserDTOs;
using Application.DTOs.Common;
using Domain.Settings;

namespace Application.Interfaces.CoreSetup.UserManagement
{
    public interface IUserManagementBusiness
    {
        Task<HtmlGrid<UserListDetails>> GetUserLists(GridParam userDetails);
        Task<SystemResponse> CheckUserName(CommonDetails commonDetails);
        UserViewModel GetRequiredDetails(CommonDetails commonDetails);
        UserViewModel GetUserUpdateDetails(CommonDetails commonDetails);
        SystemResponse ManageUser(UserParams userParam);
        SystemResponse UpdateUserStatus(UserParams userParam);
    }
}
