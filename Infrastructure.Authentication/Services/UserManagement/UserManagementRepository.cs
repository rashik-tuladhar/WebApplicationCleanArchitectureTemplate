using System.Collections.Generic;
using Application.DTOs.Authentication.UserDTOs;
using Application.DTOs.Common;
using Application.Interfaces.CoreSetup.UserManagement;
using Application.Interfaces.Repositories;
using Domain.Settings;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Authentication.Services.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly ILogger _log = Log.ForContext<UserManagementRepository>();
        private readonly IDapperDao _dapperDao;

        public UserManagementRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }
        public List<UserListDetails> GetUserLists(string storedProcedureName, GridParam gridParam)
        {
            _log.Information("sp call for getting list of user with query {0} {1}", "EXEC " + storedProcedureName, JsonConvert.SerializeObject(gridParam));
            var response = _dapperDao.ExecuteQuery<UserListDetails>(storedProcedureName, gridParam);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }

        public UserViewModelDetails GetRequiredDetails(string storedProcedureName, CommonDetails commonDetails)
        {
            UserViewModelDetails userViewModelDetails = new UserViewModelDetails();
            _log.Information("sp call for getting requried details for forms with query {0} {1}", "EXEC " + storedProcedureName, JsonConvert.SerializeObject(commonDetails));
            var response = _dapperDao.ExecuteQuery<ListDropDown, ListDropDown>(storedProcedureName, commonDetails);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            userViewModelDetails.ListGender = (List<ListDropDown>)response[0];
            userViewModelDetails.ListRoles = (List<ListDropDown>)response[1];
            return userViewModelDetails;
        }
    }
}
