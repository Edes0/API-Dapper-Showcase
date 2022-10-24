namespace Mimbly.Application.Queries.Company.GetWithAllDataById;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetFilterWithAllDataByIdCompanyHandler : IRequestHandler<GetCompanyWithChildrenByIdQuery, CompanyWithChildrenByIdVm>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetFilterWithAllDataByIdCompanyHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyWithChildrenByIdVm> Handle(GetCompanyWithChildrenByIdQuery request, CancellationToken cancellationToken)
    {
        var parentWithChildren = await _companyRepository.GetParentWithChildrenById(request.Id);

        if (parentWithChildren.IsNullOrEmpty())
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyIds =
          from company in parentWithChildren
          select company.Id;

        var companies = await _companyRepository.GetCompanyDataById(companyIds);

        foreach (var company in companies)
        {
            var childCompanies = companies.Where(c => c.ParentId == company.Id).Select(c =>
            {
                c.ChildCompanyList = c.ChildCompanyList.ToList();
                return c;
            });

            company.ChildCompanyList = childCompanies.ToList();
        }

        var parentCompany = companies.Where(c => c.Id == request.Id).Select(c => c).First();

        var companyDto = _mapper.Map<CompanyDto>(parentCompany);

        return new CompanyWithChildrenByIdVm { Company = companyDto };
    }
}