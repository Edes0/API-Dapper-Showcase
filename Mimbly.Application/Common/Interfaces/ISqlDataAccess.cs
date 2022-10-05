namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISqlDataAccess
{
    string ConnectionStringName { get; set; }

    Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);
    Task SaveData<T>(string sql, T parameters);
    Task Transaction(params string[] sqlArray);
    Task SaveDataQuery(string sql);
    Task<T> LoadOneObject<T, U>(string sql, U parameter);
}