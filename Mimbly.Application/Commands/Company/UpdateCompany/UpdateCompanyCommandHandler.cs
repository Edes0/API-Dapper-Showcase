namespace Mimbly.Application.Commands.CompanyContact.UpdateCompany;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateCompanyRequest.Validate();

        var companyEntity = _mapper.Map<Company>(request.UpdateCompanyRequest);

        companyEntity.Id = request.Id;

        await _companyRepository.UpdateCompany(companyEntity);

        return Unit.Value;
    }
}
