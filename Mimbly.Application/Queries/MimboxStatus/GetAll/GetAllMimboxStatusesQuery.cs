namespace Mimbly.Application.Queries.MimboxStatus.GetAll;

using MediatR;

public record GetAllMimboxStatusesQuery : IRequest<AllMimboxStatusesVm>
{
}