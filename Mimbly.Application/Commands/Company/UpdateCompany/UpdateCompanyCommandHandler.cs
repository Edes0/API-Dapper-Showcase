namespace Mimbly.Application.Commands.Company.UpdateCompany;

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
        var companyEntity = _mapper.Map<Company>(request.UpdateCompanyRequest);

        companyEntity.Id = request.Id;

        await _companyRepository.UpdateCompany(companyEntity);

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.UpdateCompanyRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}
