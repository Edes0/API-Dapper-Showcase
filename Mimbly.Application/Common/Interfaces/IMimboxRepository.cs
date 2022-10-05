namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMimboxRepository
{
    Task<IEnumerable<Domain.Enitites.Mimbox>> GetMimblys();
    Task<IEnumerable<Domain.Enitites.Mimbox>> GetMimblysFilteredMinByAge(int age);
}