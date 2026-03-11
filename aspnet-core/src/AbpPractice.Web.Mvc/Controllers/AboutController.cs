using Abp.AspNetCore.Mvc.Authorization;
using AbpPractice.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbpPractice.Web.Controllers;

[AbpMvcAuthorize]
public class AboutController : AbpPracticeControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
