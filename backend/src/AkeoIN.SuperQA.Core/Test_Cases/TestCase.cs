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

namespace AkeoIN.SuperQA.Test_Cases
{
    [Table("sqa.models.TestCases")]
    public class TestCase : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string Steps { get; set; }

        public string TestData { get; set; }
        public string ExpectedOutcome { get; set; }

        public Guid ScenarioId { get; set; }
        [ForeignKey("ScenarioId")]
        public Scenario Scenario { get; set; }

        public ICollection<TestRun> TestRuns { get; set; }
    }


}
