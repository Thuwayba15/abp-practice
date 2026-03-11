using AbpPractice.Roles.Dto;
using System.Collections.Generic;

namespace AbpPractice.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
