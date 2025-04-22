using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.Scenarios;
using AkeoIN.SuperQA.TestRuns;
using AkeoIN.SuperQA.EntityMapping;

namespace AkeoIN.SuperQA.Test_Cases
{
    [Table("sqa.TestCases")]
    public class TestCase : FullAuditedEntity<int>
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

        public int ScenarioId { get; set; }

        [ForeignKey("ScenarioId")]
        public Scenario Scenario { get; set; }

        public ICollection<TestRun> TestRuns { get; set; }

        public ICollection<TestPlanTestCase> TestPlanTestCases { get; set; }

        public TestCase()
        {
            TestRuns = new HashSet<TestRun>();
            TestPlanTestCases = new HashSet<TestPlanTestCase>();
        }
    }
}
