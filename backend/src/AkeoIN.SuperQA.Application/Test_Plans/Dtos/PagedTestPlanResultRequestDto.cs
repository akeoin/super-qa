using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.Test_Plans.Dtos
{
    public class PagedTestPlanResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
    }
} 