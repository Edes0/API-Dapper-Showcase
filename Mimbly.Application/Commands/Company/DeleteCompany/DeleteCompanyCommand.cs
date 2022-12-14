namespace Mimbly.Application.Commands.Company.DeleteCompany;

using System;
using MediatR;

public class DeleteCompanyCommand : IRequest
{
    public Guid Id { get; init; }
}
