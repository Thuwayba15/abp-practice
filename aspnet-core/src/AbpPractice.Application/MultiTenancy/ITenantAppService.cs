using Abp.Application.Services;
using AbpPractice.MultiTenancy.Dto;

namespace AbpPractice.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

