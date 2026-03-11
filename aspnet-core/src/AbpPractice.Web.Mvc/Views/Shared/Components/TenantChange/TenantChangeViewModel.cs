using Abp.AutoMapper;
using AbpPractice.Sessions.Dto;

namespace AbpPractice.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
