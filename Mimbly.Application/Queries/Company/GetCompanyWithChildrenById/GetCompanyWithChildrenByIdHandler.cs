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
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetCompanyWithChildrenByIdHandler(
        ICompanyRepository companyRepository,
        ICompanyContactRepository companyContactRepository,
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _companyContactRepository = companyContactRepository;
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
        var companyContacts = await _companyContactRepository.GetCompanyContactsByCompanyIds(companyIds);
        var mimboxes = await _mimboxRepository.GetMimboxesByCompanyIds(companyIds);

        foreach (var company in companiesWithData)
        {
            var currentCompanyMimboxes = mimboxes.Where(x => x.CompanyId == company.Id).Select(x => x);
            if (currentCompanyMimboxes != null)
                company.MimboxList = currentCompanyMimboxes.ToList();

            var childCompanies = companiesWithData.Where(c => c.ParentId == company.Id).Select(c => c);
            if (childCompanies != null)
                company.ChildCompanyList = childCompanies.ToList();

            var currentCompanyContacts = companyContacts.Where(x => x.CompanyId == company.Id).Select(x => x);
            if (currentCompanyContacts != null)
                company.ContactList = currentCompanyContacts.ToList();
        }

        var parentCompany = companiesWithData.Where(c => c.Id == request.Id).Select(c => c).First();

        var companyDto = _mapper.Map<CompanyDto>(parentCompany);

        return new CompanyWithChildrenByIdVm { Company = companyDto };
    }
}