using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Test_Plans;
using AkeoIN.SuperQA.TestRuns;

namespace AkeoIN.SuperQA.TestRuns.Dtos
{
    [AutoMapTo(typeof(TestRun))]
    public class CreateTestRunDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public TestPlan TestPlanId { get; set; }
    }
} 