namespace Mimbly.Application.Queries.MimboxLocation.GetAll;

using MediatR;

public record GetAllMimboxLocationsQuery : IRequest<AllMimboxLocationsVm>
{
}