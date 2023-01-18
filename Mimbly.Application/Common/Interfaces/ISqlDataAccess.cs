namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISqlDataAccess
{
    Task<T> LoadEntity<T, U>(string sql, U parameter);
    Task<IEnumerable<T>> LoadEntities<T, U>(string sql, U parameters);
    Task SaveChanges<T>(string sql, T parameters);
    Task Transaction<T>(string sql, List<T> objectArray);
}