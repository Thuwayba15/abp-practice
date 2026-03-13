using Abp.Application.Services;
using AbpPractice.Authorization.Accounts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpPractice.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<List<AvailableTenantDto>> GetActiveTenants();

    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
