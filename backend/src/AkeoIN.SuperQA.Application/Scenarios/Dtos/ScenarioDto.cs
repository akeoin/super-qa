using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Scenarios;

namespace AkeoIN.SuperQA.Scenarios.Dtos
{
    [AutoMapFrom(typeof(Scenario))]
    public class ScenarioDto : EntityDto<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int FeatureId { get; set; }
        
        public string FeatureName { get; set; }
    }
} 