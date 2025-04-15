using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AkeoIN.SuperQA.Roles.Dto;

namespace AkeoIN.SuperQA.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
    }
}
