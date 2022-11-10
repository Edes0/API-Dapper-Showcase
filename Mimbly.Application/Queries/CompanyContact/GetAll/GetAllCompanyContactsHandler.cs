namespace Mimbly.Application.Queries.CompanyContact.GetAll;

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class GetAllCompanyContactsHandler : IRequestHandler<GetAllCompanyContactsQuery, AllCompanyContactsVm>
{
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMapper _mapper;

    public GetAllCompanyContactsHandler(
        ICompanyContactRepository companyContactRepository,
        IMapper mapper)
    {
        _companyContactRepository = companyContactRepository;
        _mapper = mapper;
    }

    public async Task<AllCompanyContactsVm> Handle(GetAllCompanyContactsQuery request, CancellationToken cancellationToken)
    {
        var companyContacts = await _companyContactRepository.GetAllCompanyContacts();

        var companyContactsDtos = _mapper.Map<IEnumerable<CompanyContactDto>>(companyContacts);

        return new AllCompanyContactsVm { CompanyContacts = companyContactsDtos };
    }
}