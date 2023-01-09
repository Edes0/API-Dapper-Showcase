namespace Mimbly.Application.Commands.AD.InviteUserToAd;

using AutoMapper;
using Business.Interfaces.AD;
using Domain.Entities.AD;
using MediatR;

public class InviteUserToAdCommandHandler : IRequestHandler<InviteUserToAdCommand, bool>
{
    private readonly IAccountService _ac;
    private readonly IMapper _mapper;

    public InviteUserToAdCommandHandler(IAccountService ac, IMapper mapper)
    {
        _ac = ac;
        _mapper = mapper;
    }

    public async Task<bool> Handle(InviteUserToAdCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<AdUser>(request.InviteUserRequestToAdRequest);
        return await _ac.InviteUser(user);
    }
}