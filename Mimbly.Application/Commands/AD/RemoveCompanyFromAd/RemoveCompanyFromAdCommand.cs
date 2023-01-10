namespace Mimbly.Application.Commands.AD.RemoveCompanyFromAd;

using MediatR;

public class RemoveCompanyFromAdCommand : IRequest
{
    public Guid Id { get; init; }
}