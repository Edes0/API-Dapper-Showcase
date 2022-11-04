namespace Mimbly.Application.Commands.Company.CreateCompany;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        await request.CreateCompanyRequest.Validate();

        var companyEntity = _mapper.Map<Company>(request.CreateCompanyRequest);

        await _companyRepository.CreateCompany(companyEntity);

        return Unit.Value;
    }
}
