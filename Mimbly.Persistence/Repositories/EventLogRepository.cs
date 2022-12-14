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
