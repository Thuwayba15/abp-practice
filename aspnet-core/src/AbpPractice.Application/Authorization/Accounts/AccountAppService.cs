using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Zero.Configuration;
using AbpPractice.Authorization.Accounts.Dto;
using AbpPractice.Authorization.Users;
using AbpPractice.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpPractice.Authorization.Accounts;

public class AccountAppService : AbpPracticeAppServiceBase, IAccountAppService
{
    // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
    public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

    private readonly UserRegistrationManager _userRegistrationManager;
    private readonly IRepository<Tenant, int> _tenantRepository;

    public AccountAppService(
        UserRegistrationManager userRegistrationManager,
        IRepository<Tenant, int> tenantRepository)
    {
        _userRegistrationManager = userRegistrationManager;
        _tenantRepository = tenantRepository;
    }

    [AbpAllowAnonymous]
    public async Task<List<AvailableTenantDto>> GetActiveTenants()
    {
        return await _tenantRepository
            .GetAll()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new AvailableTenantDto
            {
                Id = x.Id,
                Name = x.Name,
                TenancyName = x.TenancyName
            })
            .ToListAsync();
    }

    [AbpAllowAnonymous]
    public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
    {
        var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
        if (tenant == null)
        {
            return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
        }

        if (!tenant.IsActive)
        {
            return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
        }

        return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
    }

    [AbpAllowAnonymous]
    public async Task<RegisterOutput> Register(RegisterInput input)
    {
        var user = await _userRegistrationManager.RegisterAsync(
            input.Name,
            input.Surname,
            input.EmailAddress,
            input.UserName,
            input.Password,
            true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
        );

        var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

        return new RegisterOutput
        {
            CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
        };
    }
}
