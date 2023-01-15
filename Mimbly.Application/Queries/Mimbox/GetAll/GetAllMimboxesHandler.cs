namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class GetAllMimboxesHandler : IRequestHandler<GetAllMimboxesQuery, AllMimboxesVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMimboxErrorLogRepository _mimboxErrorLogRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxesHandler(
        IMimboxRepository mimboxRepository,
        IMimboxErrorLogRepository mimboxErrorLogRepository,
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mimboxErrorLogRepository = mimboxErrorLogRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxesVm> Handle(GetAllMimboxesQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxRepository.GetAllMimboxes();
        var mimboxIds = mimboxes.Select(x => x.Id);
        var errorLogs = await _mimboxErrorLogRepository.GetErrorLogsByMimboxIds(mimboxIds);
        var companyIds = mimboxes.Where(x => x.Company != null).Select(x => (Guid)x.CompanyId);
        var companies = await _companyRepository.GetCompanyByIds(companyIds);

        foreach (var mimbox in mimboxes)
        {
            var currentMimboxCompany = companies.FirstOrDefault(x => x.Id == mimbox.CompanyId);
            if (currentMimboxCompany != null)
                mimbox.Company = currentMimboxCompany;

            var currentMimboxErrorLogList = errorLogs.Where(x => x.MimboxId == mimbox.Id).Select(x => x);
            if (currentMimboxCompany != null)
                mimbox.ErrorLogList = currentMimboxErrorLogList.ToList();
        }

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new AllMimboxesVm { Mimboxes = mimboxDtos };
    }
}