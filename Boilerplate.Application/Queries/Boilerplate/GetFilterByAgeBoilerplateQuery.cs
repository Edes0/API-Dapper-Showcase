namespace Boilerplate.Application.Queries.Boilerplate;

using MediatR;

public class GetBoilerplateByMinAgeQuery : IRequest<BoilerplatesFilteredByAgeVm>
{
    public int Age { get; set; }
}