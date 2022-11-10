namespace Mimbly.Application.Commands.Company.DeleteCompany;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(
        ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var mimbox = await _companyRepository.GetCompanyById(request.Id);

        if (mimbox == null)
            throw new NotFoundException($"Can't find company with id: {request.Id}");

        await _companyRepository.DeleteCompany(mimbox);

        return Unit.Value;
    }
}
