namespace Boilerplate.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBoilerplateRepository
{
    Task<IEnumerable<Domain.Models.Boilerplate>> GetBoilerPlates();
    Task<IEnumerable<Domain.Models.Boilerplate>> GetBoilerPlatesFilteredMinByAge(int age);
}