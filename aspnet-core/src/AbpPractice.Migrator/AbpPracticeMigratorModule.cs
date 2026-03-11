using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.Configuration;
using AbpPractice.EntityFrameworkCore;
using AbpPractice.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace AbpPractice.Migrator;

[DependsOn(typeof(AbpPracticeEntityFrameworkModule))]
public class AbpPracticeMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public AbpPracticeMigratorModule(AbpPracticeEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(AbpPracticeMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            AbpPracticeConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(AbpPracticeMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
