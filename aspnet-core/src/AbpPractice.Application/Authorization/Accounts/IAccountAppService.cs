using Abp.Application.Services;
using AbpPractice.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace AbpPractice.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
