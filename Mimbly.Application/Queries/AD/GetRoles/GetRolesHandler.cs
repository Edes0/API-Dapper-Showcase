namespace Mimbly.Application.Queries.AD.GetRoles;

using System.Threading;
using AutoMapper;
using MediatR;
using Mimbly.Application.Contracts.Dtos.AD;
using Mimbly.Business.Interfaces.AD;
using Mimbly.Domain.Entities.AD;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, RolesVm>
{
    private readonly IAccountService _ac;
    private readonly IMapper _mapper;

    public GetRolesHandler(IAccountService ac, IMapper mapper) {
        _ac = ac;
        _mapper = mapper;
    }


    public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken) {
        var rolesArr = await _ac.GetRoles();
        var roles = _mapper.Map<IList<AdRole>, IEnumerable<RoleDto>>(rolesArr);

        return new RolesVm { Roles = roles };
    }
}
