namespace Mimbly.Application.Commands.CompanyContact.DeleteCompanyContact;

using System;
using MediatR;

public class DeleteCompanyContactCommand : IRequest
{
    public Guid Id { get; init; }
}
