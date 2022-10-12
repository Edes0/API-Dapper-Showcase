namespace Mimbly.Application.Queries.Mimbox.GetAll;

using MediatR;

public record GetAllMimboxesQuery : IRequest<MimboxesNotFiltered>
{
}