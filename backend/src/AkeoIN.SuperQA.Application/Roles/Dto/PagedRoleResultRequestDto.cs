using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

