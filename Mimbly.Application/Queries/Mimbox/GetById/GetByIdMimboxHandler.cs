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
    private readonly IMimboxContactRepository _mimboxContactRepository;
    private readonly IMimboxErrorLogRepository _mimboxErrorLogRepository;
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxHandler(
        IMimboxRepository mimboxRepository,
        IMimboxLogRepository mimboxLogRepository,
        IMimboxContactRepository mimboxContactRepository,
        IMimboxErrorLogRepository mimboxErrorLogRepository,
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mimboxLogRepository = mimboxLogRepository;
        _mimboxContactRepository = mimboxContactRepository;
        _mimboxErrorLogRepository = mimboxErrorLogRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<MimboxByIdVm> Handle(GetByIdMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox == null)
            throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        if (mimbox.Company != null)
            mimbox.Company = await _companyRepository.GetCompanyById(mimbox.Company.Id);

        var contactList = await _mimboxContactRepository.GetMimboxContactsByMimboxId(mimbox.Id);
        if (mimbox.ContactList != null)
            mimbox.ContactList = contactList.ToList();

        var errorLogList = await _mimboxErrorLogRepository.GetErrorLogsByMimboxId(mimbox.Id);
        if (mimbox.ErrorLogList != null)
            mimbox.ErrorLogList = errorLogList.ToList();

        var logList = await _mimboxLogRepository.GetMimboxLogsByMimboxId(mimbox.Id);
        if (mimbox.LogList != null)
            mimbox.LogList = logList.ToList();

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox);

        return new MimboxByIdVm { Mimbox = mimboxDto };
    }
}