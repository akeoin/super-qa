using System.Threading.Tasks;
using Abp.Application.Services;
using AkeoIN.SuperQA.Sessions.Dto;

namespace AkeoIN.SuperQA.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
