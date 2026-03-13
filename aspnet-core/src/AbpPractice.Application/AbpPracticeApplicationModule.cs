using Abp.AutoMapper;
using Abp.FluentValidation;
using Abp.FluentValidation.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.Authorization;

namespace AbpPractice;

[DependsOn(
    typeof(AbpPracticeCoreModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFluentValidationModule))]
public class AbpPracticeApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        
        Configuration.Authorization.Providers.Add<AbpPracticeAuthorizationProvider>();
    }

/*************  ✨ Windsurf Command ⭐  *************/
        /// <summary>
        /// Initializes the module.
        /// </summary>
        /// <remarks>
        /// Registers all types in this assembly to the IoC container and
        /// configures AutoMapper to add all mappings from this assembly.
/*******  cf587d09-ba34-406b-8955-1aaa250e5666  *******/    public override void Initialize()
    {
        var thisAssembly = typeof(AbpPracticeApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
