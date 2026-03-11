using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AbpPractice.Web.Startup;

[DependsOn(typeof(AbpPracticeWebCoreModule))]
public class AbpPracticeWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public AbpPracticeWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<AbpPracticeNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(AbpPracticeWebMvcModule).GetAssembly());
    }
}
