namespace Mimbly.Application.Commands.AD.AddCompanyToAd;

using Contracts.Dtos.AD;
using MediatR;

public class AddCompanyToAdCommand : IRequest<string?>
{
    public AddCompanyDto AddCompanyToAdRequest { get; init; } = null!;
}