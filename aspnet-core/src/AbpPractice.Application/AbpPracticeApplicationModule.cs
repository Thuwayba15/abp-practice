using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.Authorization;

namespace AbpPractice;

[DependsOn(
    typeof(AbpPracticeCoreModule),
    typeof(AbpAutoMapperModule))]
public class AbpPracticeApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<AbpPracticeAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(AbpPracticeApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
