namespace Mimbly.Persistence.Repositories;

using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;

public class EventLogRepository : IEventLogRepository
{
    private readonly ISqlDataAccess _db;

    public EventLogRepository(
       ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetEventLogByMimboxId</c> queries the database
    /// for a list of <c>logs</c> using the provided <c>mimboxId</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>logs</c>.</returns>
    public async Task<IEnumerable<string>> GetEventLogByMimboxId(Guid id)
    {
        var sql =
        @"
        SELECT Log
        FROM Event_Log
        WHERE Mimbox_Id = @id
        ";

        return await _db.LoadEntities<string, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>GetTop10EventLogByMimboxId</c> queries the database
    /// for a list of the 10 latest <c>logs</c> using the provided <c>mimboxId</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>logs</c>.</returns>
    public async Task<IEnumerable<string>> GetTop10EventLogByMimboxId(Guid id)
    {
        var sql =
        @"
        SELECT TOP(10) Log
        FROM Event_Log
        WHERE Mimbox_Id = @id
        ORDER BY Created DESC
        ";

        return await _db.LoadEntities<string, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>GetEventLogBetweenDatesByMimboxId</c> queries the database
    /// for a list of <c>logs</c> created between two dates,
    /// using the provided <c>mimboxId</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <param name="startDate">The start of the interval.</param>
    /// <param name="endDate">The end of the interval</param>
    /// <returns>A <c>IEnumerable</c> of <c>logs</c>.</returns>
    public async Task<IEnumerable<string>> GetEventLogBetweenDatesByMimboxId(Guid id, DateTime startDate, DateTime endDate)
    {
        var sql =
        @"
        SELECT Log
        FROM Event_Log
        WHERE Mimbox_Id = @id
        AND Created BETWEEN @startDate AND @endDate
        ORDER BY Created DESC
        ";

        return await _db.LoadEntities<string, dynamic>(sql, new { Id = id, StartDate = startDate, EndDate = endDate });
    }
}
