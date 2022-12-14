namespace Mimbly.Application.Queries.CompanyContact.GetById;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.CoreServices.Exceptions;

public class GetByIdCompanyContactHandler : IRequestHandler<GetByIdCompanyContactQuery, CompanyContactByIdVm>
{
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMapper _mapper;

    public GetByIdCompanyContactHandler(
        ICompanyContactRepository companyContactRepository,
        IMapper mapper)
    {
        _companyContactRepository = companyContactRepository;
        _mapper = mapper;
    }

    public async Task<CompanyContactByIdVm> Handle(GetByIdCompanyContactQuery request, CancellationToken cancellationToken)
    {
        var companyContact = await _companyContactRepository.GetCompanyContactById(request.Id);

        if (companyContact == null)
            throw new NotFoundException($"Can't find company contact with id: {request.Id}");

        var companyContactDto = _mapper.Map<CompanyContactDto>(companyContact);

        return new CompanyContactByIdVm { CompanyContact = companyContactDto };
    }
}
