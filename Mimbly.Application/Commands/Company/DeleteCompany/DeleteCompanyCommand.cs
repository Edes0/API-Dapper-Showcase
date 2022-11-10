namespace Mimbly.Application.Commands.CompanyContact.DeleteCompany;

using System;
using MediatR;

public class DeleteCompanyCommand : IRequest
{
    public Guid Id { get; init; }
}
