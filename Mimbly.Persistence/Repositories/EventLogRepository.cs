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
}

