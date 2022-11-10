namespace Mimbly.Application.Queries.Company.GetCompanyWithChildrenById;

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
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetFilterWithAllDataByIdCompanyHandler(
        ICompanyRepository companyRepository,
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<CompanyWithChildrenByIdVm> Handle(GetCompanyWithChildrenByIdQuery request, CancellationToken cancellationToken)
    {
        var parentWithChildren = await _companyRepository.GetParentWithChildrenById(request.Id);

        if (parentWithChildren.IsNullOrEmpty())
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyIds = parentWithChildren.Select(x => x.Id);

        var companies = await _companyRepository.GetCompanyDataById(companyIds);
        var companiesMimboxData = await _mimboxRepository.GetMimboxDataByCompanyId(companyIds);

        foreach (var company in companies)
        {
            var currentCompanyMimboxData = companiesMimboxData.First(x => x.Id == company.Id);

            company.MimboxList = currentCompanyMimboxData.MimboxList;

            var childCompanies = companies.Where(c => c.ParentId == company.Id).Select(c =>
            {
                c.ChildCompanyList = c.ChildCompanyList;
                return c;
            });

            company.ChildCompanyList = childCompanies.ToList();
        }

        var parentCompany = companies.Where(c => c.Id == request.Id).Select(c => c).First();

        var companyDto = _mapper.Map<CompanyDto>(parentCompany);

        return new CompanyWithChildrenByIdVm { Company = companyDto };
    }
}