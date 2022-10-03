namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMimblyRepository
{
    Task<IEnumerable<Domain.Models.Mimbly>> GetMimblys();
    Task<IEnumerable<Domain.Models.Mimbly>> GetMimblysFilteredMinByAge(int age);
}