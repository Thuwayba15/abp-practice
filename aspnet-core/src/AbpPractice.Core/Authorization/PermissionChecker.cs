using Abp.Authorization;
using AbpPractice.Authorization.Roles;
using AbpPractice.Authorization.Users;

namespace AbpPractice.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
