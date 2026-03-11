using Abp.Application.Services;
using AbpPractice.Sessions.Dto;
using System.Threading.Tasks;

namespace AbpPractice.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
