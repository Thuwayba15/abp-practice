using Abp.AspNetCore.Mvc.ViewComponents;

namespace AbpPractice.Web.Views;

public abstract class AbpPracticeViewComponent : AbpViewComponent
{
    protected AbpPracticeViewComponent()
    {
        LocalizationSourceName = AbpPracticeConsts.LocalizationSourceName;
    }
}
