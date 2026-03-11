using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace AbpPractice.Web.Views;

public abstract class AbpPracticeRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected AbpPracticeRazorPage()
    {
        LocalizationSourceName = AbpPracticeConsts.LocalizationSourceName;
    }
}
