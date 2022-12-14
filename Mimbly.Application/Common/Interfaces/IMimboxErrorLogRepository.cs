namespace Mimbly.Application.Common.Interfaces;

using System.Threading.Tasks;
using Mimbly.Domain.Entities.AzureEvents;

public interface IMimboxErrorLogRepository
{
    Task UpdateMimboxErrorLog(MimboxErrorLog mimboxErrorLog);
}