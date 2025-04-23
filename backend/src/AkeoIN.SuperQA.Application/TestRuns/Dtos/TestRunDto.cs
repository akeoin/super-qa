using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AkeoIN.SuperQA.Test_Plans;
using AkeoIN.SuperQA.TestRuns;

namespace AkeoIN.SuperQA.TestRuns.Dtos
{
    [AutoMapFrom(typeof(TestRun))]
    public class TestRunDto : EntityDto<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public int TestResultCount { get; set; }
        public string TestPlan { get; set; }
    }
} 