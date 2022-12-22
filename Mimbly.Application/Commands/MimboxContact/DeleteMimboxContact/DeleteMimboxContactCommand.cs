namespace Mimbly.Application.Commands.MimboxContact.DeleteMimboxContact;

using System;
using MediatR;

public class DeleteMimboxContactCommand : IRequest
{
    public Guid Id { get; init; }
}
