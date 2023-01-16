namespace Mimbly.Application.Queries.Company.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Exceptions;

public class GetByIdCompanyHandler : IRequestHandler<GetByIdCompanyQuery, CompanyByIdVm>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetByIdCompanyHandler(
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

    public async Task<CompanyByIdVm> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyById(request.Id);
 

        if (company == null)
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        var mimboxes = await _mimboxRepository.GetMimboxesByCompanyId(company.Id);
        var companyContacts = await _companyContactRepository.GetCompanyContactsByCompanyId(company.Id);

        company.ContactList = companyContacts.ToList();
        company.MimboxList = mimboxes.ToList();

        var companyDto = _mapper.Map<CompanyDto>(company);

        return new CompanyByIdVm { Company = companyDto };
    }
}