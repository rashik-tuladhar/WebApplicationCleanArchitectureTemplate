using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;

using Serilog;
namespace Infrastructure.Persistence.Repositories
{
    public class DapperDao : IDapperDao
    {
        private readonly string _connectionString;
        private readonly ILogger _log = Log.ForContext<DapperDao>();
        public DapperDao(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<T0> ExecuteQuery<T0>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000,
                        commandType: queryType);
                    var res = result.Read<T0>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());

                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());

                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());

                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T9>().ToList());

                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T9>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T10>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T9>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T10>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T11>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T9>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T10>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T11>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T12>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T3>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T4>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T5>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T6>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T7>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T8>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T9>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T10>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T11>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T12>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T13>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public async Task<List<T0>> ExecuteQueryAsync<T0>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000,
                        commandType: queryType);
                    var res = await result.ReadAsync<T0>();
                    return res.ToList();
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public async Task<List<object>> ExecuteQueryAsync<T0, T1>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                var res = new List<object>();
                res.Add(await result.ReadAsync<T0>());
                res.Add(await result.ReadAsync<T1>());
                sqlConnection.Close();
                return res;
            }
        }
        public async Task<List<object>> ExecuteQueryAsync<T0, T1, T2>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(await result.ReadAsync<T0>());
                    if (result.IsConsumed) return res;
                    res.Add(await result.ReadAsync<T1>());
                    if (result.IsConsumed) return res;
                    res.Add(await result.ReadAsync<T2>());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }




        public string GetDataTableToString(DataTable dt)
        {
            string xml = null;
            using (TextWriter writer = new StringWriter())
            {
                dt.WriteXml(writer);
                xml = writer.ToString();
            }
            return xml;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable("dt");

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private string GetConnectionString()
        {
            return _connectionString ?? "";
        }
    }
}
