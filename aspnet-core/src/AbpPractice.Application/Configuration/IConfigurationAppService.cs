using AbpPractice.Configuration.Dto;
using System.Threading.Tasks;

namespace AbpPractice.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
