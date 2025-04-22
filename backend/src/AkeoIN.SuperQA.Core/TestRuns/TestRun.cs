using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using AkeoIN.SuperQA.TestResults;
using AkeoIN.SuperQA.Test_Cases;

namespace AkeoIN.SuperQA.TestRuns
{
    [Table("sqa.TestRuns")]
    public class TestRun : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int TestCaseId { get; set; }
        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }

        public ICollection<TestResult> TestResults { get; set; }

        public TestRun()
        {
            TestResults = new HashSet<TestResult>();
        }
    }
}
