namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISqlDataAccess
{
    string ConnectionStringName { get; set; }

    Task<T> LoadEntity<T, U>(string sql, U parameter);
    Task<IEnumerable<T>> LoadEntities<T, U>(string sql, U parameters);
    Task SaveChanges<T>(string sql, T parameters);
    Task Transaction(params string[] sqlArray);
}