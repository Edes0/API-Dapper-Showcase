namespace Mimbly.Application.Queries.Company.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Company;

public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, AllCompaniesVm>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetAllCompaniesHandler(
        ICompanyRepository companyRepository,
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<AllCompaniesVm> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompanies();
        var companyIds = companies.Select(x => x.Id);
        var mimboxes = await _mimboxRepository.GetMimboxByCompanyIds(companyIds);

        foreach (var company in companies)
        {
            var currentCompanyMimboxData = mimboxes.Select(x => x).Where(x => x.CompanyId == company.Id);

            company.MimboxList = currentCompanyMimboxData.ToList();
        }

        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return new AllCompaniesVm { Companies = companyDtos };
    }
}