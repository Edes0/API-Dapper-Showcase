namespace Mimbly.Application.Queries.MimboxContact.GetAll;

using MediatR;

public record GetAllMimboxContactsQuery : IRequest<AllMimboxContactsVm>
{
}
