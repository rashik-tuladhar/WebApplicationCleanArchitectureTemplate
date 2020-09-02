using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.CoreSetup.RoleManagement;
using Application.Interfaces.Repositories;
using Domain.Settings;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Authentication.Services.RoleManagement
{
    public class RoleManagementRepository : IRoleManagementRepository
    {
        private readonly ILogger _log = Log.ForContext<RoleManagementRepository>();
        private readonly IDapperDao _dapperDao;

        public RoleManagementRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }

        /// <summary>
        /// Returns all the available roles
        /// </summary>
        /// <returns></returns>
        public List<RoleCommonDetails> GetAvailableRoleLists(string storedProcedureName)
        {
            var param = new
            {
                Flag = "AvailableRoles"
            };
            _log.Information("sp call for getting available roles as with query {0} {1}", "EXEC " + storedProcedureName, JsonConvert.SerializeObject(param));
            var response = _dapperDao.ExecuteQuery<RoleCommonDetails>(storedProcedureName, param);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }


        /// <summary>
        /// Get Role Lists For Grid
        /// </summary>
        /// <param name="roleDetails"></param>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public List<RoleLists> GetRoleLists(GridParam roleDetails, string storedProcedureName)
        {
            _log.Information("sp call for getting available roles as with query {0} {1}", "EXEC " + storedProcedureName, JsonConvert.SerializeObject(roleDetails));
            var response = _dapperDao.ExecuteQuery<RoleLists>(storedProcedureName, roleDetails);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }

        public Tuple<List<RoleCommonDetails>, List<string>, RoleInfo> GetRoleDetailsUpdate(string roleId, string storedProcedureName)
        {
            var param = new
            {
                Flag = "RoleDetails",
                RoleId = roleId
            };
            var response = _dapperDao.ExecuteQuery<RoleCommonDetails, string, RoleInfo>(storedProcedureName, param);
            var allRoleLists = (List<RoleCommonDetails>)response[0];
            var selectedRoles = (List<string>)response[1];
            var roleInfoList = (List<RoleInfo>)response[2];
            var roleInfo = (RoleInfo)roleInfoList.FirstOrDefault();
            var returnDetails = new Tuple<List<RoleCommonDetails>, List<string>, RoleInfo>(allRoleLists, selectedRoles, roleInfo);
            return returnDetails;
        }
    }
}
