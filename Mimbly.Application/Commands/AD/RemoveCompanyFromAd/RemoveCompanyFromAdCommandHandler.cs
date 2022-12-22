namespace Mimbly.Application.Commands.AD.RemoveCompanyFromAd;

using Business.Interfaces.AD;
using MediatR;

public class RemoveCompanyFromAdCommandHandler : IRequestHandler<RemoveCompanyFromAdCommand>
{
    private readonly IAccountService _ac;

    public RemoveCompanyFromAdCommandHandler(IAccountService ac) => _ac = ac;

    public async Task<Unit> Handle(RemoveCompanyFromAdCommand request, CancellationToken cancellationToken)
    {
        await _ac.RemoveCompany(request.Id);
        return Unit.Value;
    }
}