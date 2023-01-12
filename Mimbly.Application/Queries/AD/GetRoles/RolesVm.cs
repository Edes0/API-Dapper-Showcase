
namespace Mimbly.Application.Queries.AD.GetRoles;

using Mimbly.Application.Contracts.Dtos.AD;

public class RolesVm
{
    public RolesVm() => Roles = new List<RoleDto>();
    public IEnumerable<RoleDto> Roles { get; set; }
}
