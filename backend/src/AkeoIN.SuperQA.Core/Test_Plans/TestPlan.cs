using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Test_Cases;
using System.Collections.Generic;
using AkeoIN.SuperQA.EntityMapping;

namespace AkeoIN.SuperQA.Test_Plans
{
    [Table("sqa.TestPlans")]
    public class TestPlan : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public int TestCaseId { get; set; }
        [ForeignKey("TestCaseId")]

        [Required]
        public TestCase TestCase { get; set; }

        public ICollection<TestPlanTestCase> TestPlanTestCases { get; set; }

        public TestPlan()
        {
            TestPlanTestCases = new HashSet<TestPlanTestCase>();
        }
    }
}
