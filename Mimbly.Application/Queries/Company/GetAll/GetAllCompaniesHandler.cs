namespace Mimbly.Application.Queries.Company.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, CompaniesNotFiltered>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetAllCompaniesHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompaniesNotFiltered> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompanies();

        if (companies.IsNullOrEmpty()) throw new NotFoundException("No mimboxes found in database");

        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return new CompaniesNotFiltered
        {
            Companies = companyDtos
        };
    }
}