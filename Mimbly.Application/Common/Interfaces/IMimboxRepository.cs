namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Enitites;

public interface IMimboxRepository
{
    Task<IEnumerable<Mimbox>> GetAllMimboxes();
    Task<IEnumerable<Mimbox>> GetMimblysFilteredMinByAge(int age);
}