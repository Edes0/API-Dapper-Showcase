namespace Mimbly.Application.Queries.MimboxModel.GetAll;

using MediatR;

public record GetAllMimboxModelsQuery : IRequest<AllMimboxModelsVm>
{
}
