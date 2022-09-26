namespace Boilerplate.Application.Queries.Boilerplate;

using System.Collections.Generic;
using global::Boilerplate.Application.Contracts.Dtos;

public class BoilerplatesFilteredByAgeVm
{
    public IEnumerable<BoilerplateDto> Boilerplates { get; set; }

    public BoilerplatesFilteredByAgeVm() => Boilerplates = new List<BoilerplateDto>();
}