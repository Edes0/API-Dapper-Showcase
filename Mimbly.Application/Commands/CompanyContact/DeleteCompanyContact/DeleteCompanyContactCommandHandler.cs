namespace Mimbly.Application.Commands.CompanyContact.DeleteCompanyContact;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteCompanyContactCommandHandler : IRequestHandler<DeleteCompanyContactCommand>
{
    private readonly ICompanyContactRepository _companyContactRepository;

    public DeleteCompanyContactCommandHandler(
        ICompanyContactRepository companyContactRepository)
    {
        _companyContactRepository = companyContactRepository;
    }

    public async Task<Unit> Handle(DeleteCompanyContactCommand request, CancellationToken cancellationToken)
    {
        var companyContact = await _companyContactRepository.GetCompanyContactById(request.Id);

        if (companyContact == null)
            throw new NotFoundException($"Can't find company contact with id: {request.Id}");

        await _companyContactRepository.DeleteCompanyContact(companyContact);

        return Unit.Value;
    }
}
