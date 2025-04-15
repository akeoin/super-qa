using Abp.Application.Services;
using AkeoIN.SuperQA.MultiTenancy.Dto;

namespace AkeoIN.SuperQA.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

