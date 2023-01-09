namespace Mimbly.Application.Commands.AD.AddCompanyToAd;

using Contracts.Dtos.AD;
using MediatR;

public class AddCompanyToAdCommand : IRequest<string?>
{
    public AddCompanyRequestDto AddCompanyRequestToAdRequest { get; init; } = null!;
}