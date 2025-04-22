using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Test_Cases;
using AkeoIN.SuperQA.Test_Plans;

namespace AkeoIN.SuperQA.EntityMapping
{
    [Table("sqa.TestPlanTestCases")]
    public class TestPlanTestCase : FullAuditedEntity<int>
    {
        public int TestPlanId { get; set; }

        [ForeignKey(nameof(TestPlanId))]
        public TestPlan TestPlan { get; set; }

        public int TestCaseId { get; set; }

        [ForeignKey(nameof(TestCaseId))]
        public TestCase TestCase { get; set; }
    }
}
