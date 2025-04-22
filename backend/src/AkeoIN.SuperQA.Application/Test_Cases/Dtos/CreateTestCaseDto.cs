using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Test_Cases;

namespace AkeoIN.SuperQA.Test_Cases.Dtos
{
    [AutoMapTo(typeof(TestCase))]
    public class CreateTestCaseDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Steps { get; set; }

        [StringLength(1000)]
        public string TestData { get; set; }

        [Required]
        [StringLength(1000)]
        public string ExpectedOutcome { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int ScenarioId { get; set; }
    }
} 