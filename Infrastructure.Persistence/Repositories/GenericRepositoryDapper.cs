using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.Interfaces.Repositories;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryDapper : IGenericRepositoryDapper
    {
        private readonly IDapperDao _dapperDao;
        private readonly ILogger _log = Log.ForContext<GenericRepositoryDapper>();

        public GenericRepositoryDapper(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }

        /// <summary>
        /// Generic Method For CRUD operation
        /// </summary>
        /// <typeparam name="T">Object For Stored Procedure</typeparam>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="input">Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public SystemResponse ManageData<T>(string spName, T input)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(input));
            var response = _dapperDao.ExecuteQuery<SystemResponse>(procedureName, input);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }


        /// <summary>
        /// Generic Method For CRUD operation (Asynchronous)
        /// </summary>
        /// <typeparam name="T">Object For Stored Procedure</typeparam>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="input">Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public async Task<SystemResponse> ManageDataAsync<T>(string spName, T input)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(input));
            var response = await _dapperDao.ExecuteQueryAsync<SystemResponse>(procedureName, input);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }


        /// <summary>
        /// Generic Method For Anonymous Object as a return
        /// </summary>
        /// <typeparam name="T">Object For Stored Procedure</typeparam>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="obj">Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public T ManageDataWithSingleObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }


        /// <summary>
        /// Generic Method For Anonymous Object as a return (Asynchronous)
        /// </summary>
        /// <typeparam name="T">Object For Stored Procedure</typeparam>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="obj">Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>

        public async Task<T> ManageDataWithSingleObjectAsync<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = await _dapperDao.ExecuteQueryAsync<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }


        ///<summary>
        /// Generic Method For List of Anonymous Object as a return
        /// </summary>
        /// <typeparam name = "T" > Object For Stored Procedure</typeparam>
        /// <param name = "spName" > Stored Procedure Name</param>
        /// <param name = "obj" > Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public List<T> ManageDataWithListObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }


        ///<summary>
        /// Generic Method For List of Anonymous Object as a return (Asynchronous)
        /// </summary>
        /// <typeparam name = "T" > Object For Stored Procedure</typeparam>
        /// <param name = "spName" > Stored Procedure Name</param>
        /// <param name = "obj" > Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public async Task<List<T>> ManageDataWithListObjectAsync<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp call for checking region code with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = await _dapperDao.ExecuteQueryAsync<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
