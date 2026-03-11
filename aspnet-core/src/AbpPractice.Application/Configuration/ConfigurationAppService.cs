using Abp.Authorization;
using Abp.Runtime.Session;
using AbpPractice.Configuration.Dto;
using System.Threading.Tasks;

namespace AbpPractice.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : AbpPracticeAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
