namespace Mimbly.Application.Queries.AD.GetRoles;

using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Mimbly.Application.Contracts.Dtos.AD;
using Mimbly.Business.Interfaces.AD;
using Mimbly.Domain.Entities.AD;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, RolesVm>
{
    private readonly IAccountService _ac;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public GetRolesHandler(IAccountService ac, IMapper mapper, IMemoryCache memoryCache) {
        _ac = ac;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }


    public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken) {
        var rolesInCache = _memoryCache.Get<IEnumerable<RoleDto>>("Roles");
        if (rolesInCache != null) return new RolesVm {Roles = rolesInCache};

        var rolesArr = await _ac.GetRoles();
        var roles = _mapper.Map<IList<AdRole>, IEnumerable<RoleDto>>(rolesArr);
        var memoryCacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(12));

        _memoryCache.Set<IEnumerable<RoleDto>>("Roles", roles, memoryCacheOptions);

        return new RolesVm { Roles = roles };
    }
}
