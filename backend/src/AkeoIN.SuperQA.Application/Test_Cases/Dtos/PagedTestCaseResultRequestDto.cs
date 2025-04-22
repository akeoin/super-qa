using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.Test_Cases.Dtos
{
    public class PagedTestCaseResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
        public int? ScenarioId { get; set; }
    }
} 