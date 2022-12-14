namespace Mimbly.Application.Commands.CompanyContact.UpdateCompanyContact;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateCompanyContactCommandHandler : IRequestHandler<UpdateCompanyContactCommand>
{
    private readonly ICompanyContactRepository _companyContactRepository;
    private readonly IMapper _mapper;

    public UpdateCompanyContactCommandHandler(
        ICompanyContactRepository companyContactRepository,
        IMapper mapper)
    {
        _companyContactRepository = companyContactRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCompanyContactCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateCompanyContactRequest.Validate();

        var companyContactEntity = _mapper.Map<CompanyContact>(request.UpdateCompanyContactRequest);

        companyContactEntity.Id = request.Id;

        await _companyContactRepository.UpdateCompanyContact(companyContactEntity);

        return Unit.Value;
    }
}
