namespace Mimbly.Application.Queries.MimboxModel.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxModel;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxModelHandler : IRequestHandler<GetByIdMimboxModelQuery, MimboxModelByIdVm>
{
    private readonly IMimboxModelRepository _mimboxModelRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxModelHandler(
        IMimboxModelRepository mimboxModelRepository,
        IMapper mapper)
    {
        _mimboxModelRepository = mimboxModelRepository;
        _mapper = mapper;
    }

    public async Task<MimboxModelByIdVm> Handle(GetByIdMimboxModelQuery request, CancellationToken cancellationToken)
    {
        var mimboxModel = await _mimboxModelRepository.GetMimboxModelById(request.Id);

        if (mimboxModel == null)
            throw new NotFoundException($"Can't find model with id: {request.Id}");

        var mimboxModelDto = _mapper.Map<MimboxModelDto>(mimboxModel);

        return new MimboxModelByIdVm { MimboxModel = mimboxModelDto };
    }
}