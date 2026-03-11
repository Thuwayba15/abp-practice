using AbpPractice.Roles.Dto;
using System.Collections.Generic;

namespace AbpPractice.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
