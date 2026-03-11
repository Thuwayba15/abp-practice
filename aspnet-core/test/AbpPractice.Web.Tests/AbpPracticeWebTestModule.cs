using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.EntityFrameworkCore;
using AbpPractice.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AbpPractice.Web.Tests;

[DependsOn(
    typeof(AbpPracticeWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class AbpPracticeWebTestModule : AbpModule
{
    public AbpPracticeWebTestModule(AbpPracticeEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(AbpPracticeWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(AbpPracticeWebMvcModule).Assembly);
    }
}