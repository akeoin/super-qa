using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Test_Cases;
using AkeoIN.SuperQA.Test_Plans;

namespace AkeoIN.SuperQA.Test_Plans.Dtos
{
    [AutoMapFrom(typeof(TestPlan))]
    public class TestPlanDto : EntityDto<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public TestCase TestCase { get; set; }

        public int TestCaseCount { get; set; }
    }
} 