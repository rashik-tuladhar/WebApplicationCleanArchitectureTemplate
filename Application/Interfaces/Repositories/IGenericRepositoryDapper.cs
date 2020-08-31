using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Common;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepositoryDapper
    {
        SystemResponse ManageData<T>(string spName, T input);
        Task<SystemResponse> ManageDataAsync<T>(string spName, T input);
        T ManageDataWithSingleObject<T>(string spName, object obj);
        Task<T> ManageDataWithSingleObjectAsync<T>(string spName, object obj);
        List<T> ManageDataWithListObject<T>(string spName, object obj);
        Task<List<T>> ManageDataWithListObjectAsync<T>(string spName, object obj);
    }
}
