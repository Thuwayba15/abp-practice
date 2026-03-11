using Abp.AspNetCore.Mvc.Authorization;
using AbpPractice.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbpPractice.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : AbpPracticeControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
