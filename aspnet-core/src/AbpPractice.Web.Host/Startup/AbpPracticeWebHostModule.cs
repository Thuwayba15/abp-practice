using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpPractice.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AbpPractice.Web.Host.Startup
{
    [DependsOn(
       typeof(AbpPracticeWebCoreModule))]
    public class AbpPracticeWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AbpPracticeWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpPracticeWebHostModule).GetAssembly());
        }
    }
}
