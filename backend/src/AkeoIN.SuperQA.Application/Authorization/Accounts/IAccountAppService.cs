using System.Threading.Tasks;
using Abp.Application.Services;
using AkeoIN.SuperQA.Authorization.Accounts.Dto;

namespace AkeoIN.SuperQA.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
