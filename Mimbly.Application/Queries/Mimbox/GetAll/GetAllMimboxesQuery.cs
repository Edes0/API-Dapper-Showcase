namespace Mimbly.Application.Queries.Mimbox.GetAll;

using global::Mimbly.Application.Queries.Mimbly;
using MediatR;

public record GetAllMimboxesQuery : IRequest<MimboxesNotFiltered>
{
}