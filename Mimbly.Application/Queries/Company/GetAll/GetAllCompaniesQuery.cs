namespace Mimbly.Application.Queries.Company.GetAll;

using MediatR;

public record GetAllCompaniesQuery : IRequest<AllCompaniesVm>
{
}