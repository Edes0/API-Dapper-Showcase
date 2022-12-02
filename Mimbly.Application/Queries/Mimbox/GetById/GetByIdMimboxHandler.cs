namespace Mimbly.Application.Queries.Mimbox.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxHandler : IRequestHandler<GetByIdMimboxQuery, MimboxByIdVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxHandler(
        IMimboxRepository mimboxRepository,
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<MimboxByIdVm> Handle(GetByIdMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox == null)
            throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        mimbox.Company = await _companyRepository.GetCompanyDataById(mimbox.Company.Id);

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox);

        return new MimboxByIdVm { Mimbox = mimboxDto };
    }
}