namespace Mimbly.Application.Queries.MimboxModel.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxModel;

public class GetAllMimboxModelsHandler : IRequestHandler<GetAllMimboxModelsQuery, AllMimboxModelsVm>
{
    private readonly IMimboxModelRepository _mimboxModelRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxModelsHandler(
        IMimboxModelRepository mimboxModelRepository,
        IMapper mapper)
    {
        _mimboxModelRepository = mimboxModelRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxModelsVm> Handle(GetAllMimboxModelsQuery request, CancellationToken cancellationToken)
    {
        var mimboxModels = await _mimboxModelRepository.GetAllMimboxModels();

        var mimboxModelDtos = _mapper.Map<IEnumerable<MimboxModelDto>>(mimboxModels);

        return new AllMimboxModelsVm { MimboxModels = mimboxModelDtos };
    }
}