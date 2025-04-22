using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Scenarios;

namespace AkeoIN.SuperQA.Scenarios.Dtos
{
    [AutoMapTo(typeof(Scenario))]
    public class CreateScenarioDto
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
    }
} 