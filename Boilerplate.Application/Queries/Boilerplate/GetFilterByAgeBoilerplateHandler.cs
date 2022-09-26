namespace Boilerplate.Application.Queries.Boilerplate;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Boilerplate.Application.Common.Interfaces;
using global::Boilerplate.Application.Contracts.Dtos;
using MediatR;

public class GetBoilerplateByMinAgeHandler : IRequestHandler<GetBoilerplateByMinAgeQuery, BoilerplatesFilteredByAgeVm>
{
    private readonly IBoilerplateRepository _boilerplateRepository;
    private readonly IMapper _mapper;

    public GetBoilerplateByMinAgeHandler(
        IBoilerplateRepository boilerplateRepository,
        IMapper mapper)
    {
        _boilerplateRepository = boilerplateRepository;
        _mapper = mapper;
    }

    public async Task<BoilerplatesFilteredByAgeVm> Handle(GetBoilerplateByMinAgeQuery request, CancellationToken cancellationToken)
    {
        var boilerplates = await _boilerplateRepository.GetBoilerPlatesFilteredMinByAge(request.Age);

        var boilerplateDtos = _mapper.Map<IEnumerable<BoilerplateDto>>(boilerplates);

        return new BoilerplatesFilteredByAgeVm
        {
            Boilerplates = boilerplateDtos
        };
    }
}