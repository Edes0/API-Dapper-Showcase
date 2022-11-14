namespace Mimbly.Application.Queries.CompanyContact.GetById;

using System;
using MediatR;

public record GetByIdCompanyContactQuery : IRequest<CompanyContactByIdVm>
{
    public Guid Id { get; set; }
}
