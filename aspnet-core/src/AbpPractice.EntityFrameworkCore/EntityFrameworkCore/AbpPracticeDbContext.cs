using Abp.Zero.EntityFrameworkCore;
using AbpPractice.Authorization.Roles;
using AbpPractice.Authorization.Users;
using AbpPractice.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace AbpPractice.EntityFrameworkCore;

public class AbpPracticeDbContext : AbpZeroDbContext<Tenant, Role, User, AbpPracticeDbContext>
{
    /* Define a DbSet for each entity of the application */

    public AbpPracticeDbContext(DbContextOptions<AbpPracticeDbContext> options)
        : base(options)
    {
    }
}
