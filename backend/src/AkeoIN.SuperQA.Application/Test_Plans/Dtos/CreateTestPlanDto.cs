using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Test_Cases;

namespace AkeoIN.SuperQA.Test_Plans.Dtos
{
    [AutoMapTo(typeof(TestPlan))]
    public class CreateTestPlanDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public string Status { get; set; }

        public int TestCaseId { get; set; }

        public TestCase TestCase { get; set; }
    }
} 