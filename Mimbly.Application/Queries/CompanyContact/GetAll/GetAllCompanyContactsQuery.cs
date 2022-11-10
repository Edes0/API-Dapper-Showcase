namespace Mimbly.Application.Queries.CompanyContact.GetAll;

using MediatR;

public record GetAllCompanyContactsQuery : IRequest<AllCompanyContactsVm>
{
}
