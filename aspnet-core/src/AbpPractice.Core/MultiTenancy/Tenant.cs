using Abp.MultiTenancy;
using AbpPractice.Authorization.Users;

namespace AbpPractice.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
