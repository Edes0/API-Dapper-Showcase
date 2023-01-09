namespace Mimbly.Application.Queries.MimboxContact.GetById;

using System;
using MediatR;

public record GetByIdMimboxContactQuery : IRequest<MimboxContactByIdVm>
{
    public Guid Id { get; set; }
}
