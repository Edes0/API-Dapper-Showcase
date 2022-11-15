namespace Mimbly.Application.Commands.CompanyContact.CreateCompanyContact;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyContactCommand, CompanyContact>
{
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(
        ICompanyContactRepository companyContactRepository,
        IMapper mapper)
    {
        _companyContactRepository = companyContactRepository;
        _mapper = mapper;
    }

    public async Task<CompanyContact> Handle(CreateCompanyContactCommand request, CancellationToken cancellationToken)
    {
        await request.CreateCompanyContactRequest.Validate();

        var companyContactEntity = _mapper.Map<CompanyContact>(request.CreateCompanyContactRequest);

        await _companyContactRepository.CreateCompanyContact(companyContactEntity);

        return companyContactEntity;
    }
}
