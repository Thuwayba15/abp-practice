using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AbpPractice.Controllers
{
    public abstract class AbpPracticeControllerBase : AbpController
    {
        protected AbpPracticeControllerBase()
        {
            LocalizationSourceName = AbpPracticeConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
