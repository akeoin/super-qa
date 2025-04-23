using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.TestRuns.Dtos
{
    public class PagedTestRunResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? TestCaseId { get; set; }
    }
} 