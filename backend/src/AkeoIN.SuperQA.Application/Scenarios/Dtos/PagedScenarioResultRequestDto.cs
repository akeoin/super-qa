using Abp.Application.Services.Dto;

namespace AkeoIN.SuperQA.Scenarios.Dtos
{
    public class PagedScenarioResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
        public int? FeatureId { get; set; }
    }
} 