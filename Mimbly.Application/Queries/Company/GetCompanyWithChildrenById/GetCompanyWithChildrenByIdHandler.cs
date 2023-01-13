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

public class GetCompanyWithChildrenByIdHandler : IRequestHandler<GetCompanyWithChildrenByIdQuery, CompanyWithChildrenByIdVm>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetCompanyWithChildrenByIdHandler(
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
        var companies = await _companyRepository.GetParentAndChildrenIdsById(request.Id);

        if (companies.IsNullOrEmpty())
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var companyIds = companies.Select(x => x.Id);
        var companiesWithData = await _companyRepository.GetCompanyByIds(companyIds);

        var mimboxes = await _mimboxRepository.GetMimboxByCompanyIds(companyIds);

        foreach (var company in companiesWithData)
        {
            var currentCompanyMimboxes = mimboxes.Select(x => x).Where(x => x.CompanyId == company.Id);

            company.MimboxList = currentCompanyMimboxes.ToList();

            var childCompanies = companiesWithData.Where(c => c.ParentId == company.Id).Select(c => c);

            company.ChildCompanyList = childCompanies.ToList();
        }

        var parentCompany = companiesWithData.Where(c => c.Id == request.Id).Select(c => c).First();

        var companyDto = _mapper.Map<CompanyDto>(parentCompany);

        return new CompanyWithChildrenByIdVm { Company = companyDto };
    }
}